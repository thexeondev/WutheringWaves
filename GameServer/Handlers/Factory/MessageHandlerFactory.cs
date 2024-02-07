using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using GameServer.Handlers.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Protocol;

namespace GameServer.Handlers.Factory;
internal class MessageHandlerFactory
{
    private readonly ImmutableDictionary<MessageId, MessageHandler> s_messageHandlers;

    public MessageHandlerFactory(ILogger<MessageHandlerFactory> logger)
    {
        IEnumerable<Type> handlerTypes = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.IsAssignableTo(typeof(MessageHandlerBase)) && !t.IsAbstract);

        s_messageHandlers = GenerateHandlerMethods(handlerTypes);
        logger.LogInformation("Registered {count} message handlers", s_messageHandlers.Count);
    }

    public MessageHandler? GetHandler(MessageId messageId)
    {
        s_messageHandlers.TryGetValue(messageId, out MessageHandler? handler);
        return handler;
    }

    private static ImmutableDictionary<MessageId, MessageHandler> GenerateHandlerMethods(IEnumerable<Type> handlerTypes)
    {
        var builder = ImmutableDictionary.CreateBuilder<MessageId, MessageHandler>();

        MethodInfo getServiceMethod = typeof(ServiceProviderServiceExtensions).GetMethod("GetRequiredService", [typeof(IServiceProvider)])!;
        foreach (Type type in handlerTypes)
        {
            IEnumerable<MethodInfo> methods = type.GetMethods()
                                   .Where(method => method.GetCustomAttribute<MessageHandlerAttribute>() != null);

            foreach (MethodInfo method in methods)
            {
                MessageHandlerAttribute attribute = method.GetCustomAttribute<MessageHandlerAttribute>()!;

                ParameterExpression serviceProviderParam = Expression.Parameter(typeof(IServiceProvider));
                ParameterExpression dataParam = Expression.Parameter(typeof(ReadOnlyMemory<byte>));

                MethodCallExpression getServiceCall = Expression.Call(getServiceMethod.MakeGenericMethod(type), serviceProviderParam);
                MethodCallExpression handlerCall = Expression.Call(getServiceCall, method, dataParam);

                Expression<MessageHandler> lambda = Expression.Lambda<MessageHandler>(handlerCall, serviceProviderParam, dataParam);
                builder.Add(attribute.MessageId, lambda.Compile());
            }
        }

        return builder.ToImmutable();
    }
}
