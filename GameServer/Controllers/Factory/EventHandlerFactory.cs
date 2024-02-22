using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using GameServer.Controllers.Attributes;
using GameServer.Network.Messages;
using GameServer.Systems.Event;
using Google.Protobuf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Protocol;

namespace GameServer.Controllers.Factory;

internal delegate Task GameEventHandler(IServiceProvider serviceProvider);
internal class EventHandlerFactory
{
    private readonly ImmutableDictionary<MessageId, RpcHandler> _rpcHandlers;
    private readonly ImmutableDictionary<MessageId, PushHandler> _pushHandlers;
    private readonly ImmutableDictionary<GameEventType, List<GameEventHandler>> _eventHandlers;

    public EventHandlerFactory(ILogger<EventHandlerFactory> logger)
    {
        IEnumerable<Type> controllerTypes = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.IsAssignableTo(typeof(Controller)) && !t.IsAbstract);

        _rpcHandlers = RegisterRpcHandlers(controllerTypes);
        _pushHandlers = RegisterPushHandlers(controllerTypes);
        _eventHandlers = RegisterEventHandlers(controllerTypes);
        logger.LogInformation("Registered {rpc_count} rpc handlers, {push_count} push handlers", _rpcHandlers.Count, _pushHandlers.Count);
    }

    public RpcHandler? GetRpcHandler(MessageId messageId)
    {
        _rpcHandlers.TryGetValue(messageId, out RpcHandler? handler);
        return handler;
    }

    public PushHandler? GetPushHandler(MessageId messageId)
    {
        _pushHandlers.TryGetValue(messageId, out PushHandler? handler);
        return handler;
    }

    public IEnumerable<GameEventHandler> GetEventHandlers(GameEventType eventType)
    {
        if (!_eventHandlers.TryGetValue(eventType, out List<GameEventHandler>? handlers))
            return [];

        return handlers;
    }

    private static ImmutableDictionary<GameEventType, List<GameEventHandler>> RegisterEventHandlers(IEnumerable<Type> controllerTypes)
    {
        var builder = ImmutableDictionary.CreateBuilder<GameEventType, List<GameEventHandler>>();

        MethodInfo getServiceMethod = typeof(ServiceProviderServiceExtensions).GetMethod(nameof(ServiceProviderServiceExtensions.GetRequiredService), [typeof(IServiceProvider)])!;
        MethodInfo taskFromResultMethod = typeof(Task).GetMethod(nameof(Task.FromResult))!.MakeGenericMethod(typeof(ResponseMessage));

        foreach (Type type in controllerTypes)
        {
            IEnumerable<MethodInfo> methods = type.GetMethods()
                                   .Where(method => method.GetCustomAttribute<GameEventAttribute>() != null
                                   && (method.ReturnType == typeof(Task) || method.ReturnType == typeof(void)));

            foreach (MethodInfo method in methods)
            {
                GameEventAttribute attribute = method.GetCustomAttribute<GameEventAttribute>()!;
                ParameterExpression serviceProviderParam = Expression.Parameter(typeof(IServiceProvider));

                MethodCallExpression getServiceCall = Expression.Call(getServiceMethod.MakeGenericMethod(type), serviceProviderParam);
                Expression handlerCall = Expression.Call(getServiceCall, method, FetchArgumentsForMethod(method, serviceProviderParam, getServiceMethod));

                if (method.ReturnType == typeof(void)) // Allow non-async methods as well
                    handlerCall = Expression.Block(handlerCall, Expression.Constant(Task.CompletedTask));

                Expression<GameEventHandler> lambda = Expression.Lambda<GameEventHandler>(handlerCall, serviceProviderParam);

                if (!builder.TryGetKey(attribute.Type, out _))
                    builder.Add(attribute.Type, []);

                builder[attribute.Type].Add(lambda.Compile());
            }
        }

        return builder.ToImmutable();
    }

    private static ImmutableDictionary<MessageId, RpcHandler> RegisterRpcHandlers(IEnumerable<Type> controllerTypes)
    {
        var builder = ImmutableDictionary.CreateBuilder<MessageId, RpcHandler>();

        MethodInfo getServiceMethod = typeof(ServiceProviderServiceExtensions).GetMethod(nameof(ServiceProviderServiceExtensions.GetRequiredService), [typeof(IServiceProvider)])!;
        MethodInfo taskFromResultMethod = typeof(Task).GetMethod(nameof(Task.FromResult))!.MakeGenericMethod(typeof(RpcResult));

        foreach (Type type in controllerTypes)
        {
            IEnumerable<MethodInfo> methods = type.GetMethods()
                                   .Where(method => method.GetCustomAttribute<NetEventAttribute>() != null
                                   && (method.ReturnType == typeof(Task<RpcResult>) || method.ReturnType == typeof(RpcResult)));

            foreach (MethodInfo method in methods)
            {
                NetEventAttribute attribute = method.GetCustomAttribute<NetEventAttribute>()!;

                ParameterExpression serviceProviderParam = Expression.Parameter(typeof(IServiceProvider));
                ParameterExpression dataParam = Expression.Parameter(typeof(ReadOnlySpan<byte>));

                MethodCallExpression getServiceCall = Expression.Call(getServiceMethod.MakeGenericMethod(type), serviceProviderParam);
                Expression handlerCall = Expression.Call(getServiceCall, method, FetchArgumentsForMethod(method, serviceProviderParam, getServiceMethod, dataParam));

                if (method.ReturnType == typeof(RpcResult)) // Allow non-async methods as well
                    handlerCall = Expression.Call(taskFromResultMethod, handlerCall);

                Expression<RpcHandler> lambda = Expression.Lambda<RpcHandler>(handlerCall, serviceProviderParam, dataParam);

                builder.Add(attribute.MessageId, lambda.Compile());
            }
        }

        return builder.ToImmutable();
    }

    private static ImmutableDictionary<MessageId, PushHandler> RegisterPushHandlers(IEnumerable<Type> controllerTypes)
    {
        var builder = ImmutableDictionary.CreateBuilder<MessageId, PushHandler>();

        MethodInfo getServiceMethod = typeof(ServiceProviderServiceExtensions).GetMethod(nameof(ServiceProviderServiceExtensions.GetRequiredService), [typeof(IServiceProvider)])!;
        MethodInfo taskFromResultMethod = typeof(Task).GetMethod(nameof(Task.FromResult))!.MakeGenericMethod(typeof(ResponseMessage));

        foreach (Type type in controllerTypes)
        {
            IEnumerable<MethodInfo> methods = type.GetMethods()
                                   .Where(method => method.GetCustomAttribute<NetEventAttribute>() != null
                                   && (method.ReturnType == typeof(Task) || method.ReturnType == typeof(void)));

            foreach (MethodInfo method in methods)
            {
                NetEventAttribute attribute = method.GetCustomAttribute<NetEventAttribute>()!;

                ParameterExpression serviceProviderParam = Expression.Parameter(typeof(IServiceProvider));
                ParameterExpression dataParam = Expression.Parameter(typeof(ReadOnlySpan<byte>));

                MethodCallExpression getServiceCall = Expression.Call(getServiceMethod.MakeGenericMethod(type), serviceProviderParam);
                Expression handlerCall = Expression.Call(getServiceCall, method, FetchArgumentsForMethod(method, serviceProviderParam, getServiceMethod, dataParam));

                if (method.ReturnType == typeof(void)) // Allow non-async methods as well
                    handlerCall = Expression.Block(handlerCall, Expression.Constant(Task.CompletedTask));

                Expression<PushHandler> lambda = Expression.Lambda<PushHandler>(handlerCall, serviceProviderParam, dataParam);

                builder.Add(attribute.MessageId, lambda.Compile());
            }
        }

        return builder.ToImmutable();
    }

    private static List<Expression> FetchArgumentsForMethod(MethodInfo method, Expression serviceProviderParam, MethodInfo getServiceMethod, Expression? dataParam = null)
    {
        List<Expression> arguments = [];
        foreach (ParameterInfo param in method.GetParameters())
        {
            if (dataParam != null && param.ParameterType.IsAssignableTo(typeof(IMessage)))
            {
                PropertyInfo parser = (param.ParameterType.GetMember("Parser", BindingFlags.Static | BindingFlags.Public).Single() as PropertyInfo)!;
                MethodInfo parseFrom = parser.PropertyType.GetMethod(nameof(MessageParser.ParseFrom), [typeof(ReadOnlySpan<byte>)])!;

                arguments.Add(Expression.Call(Expression.Constant(parser.GetValue(null)), parseFrom, dataParam));
            }
            else
            {
                arguments.Add(Expression.Call(getServiceMethod.MakeGenericMethod(param.ParameterType), serviceProviderParam));
            }
        }

        return arguments;
    }
}
