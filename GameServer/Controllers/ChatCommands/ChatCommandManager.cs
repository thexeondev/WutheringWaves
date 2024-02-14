using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using GameServer.Controllers.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Controllers.ChatCommands;
internal class ChatCommandManager
{
    private delegate Task ChatCommandDelegate(IServiceProvider serviceProvider, string[] args);
    private static readonly ImmutableDictionary<string, ImmutableDictionary<string, ChatCommandDelegate>> s_commandCategories;
    private static readonly ImmutableArray<string> s_commandDescriptions;

    private readonly IServiceProvider _serviceProvider;

    static ChatCommandManager()
    {
        (s_commandCategories, s_commandDescriptions) = RegisterCommands();
    }

    public static IEnumerable<string> CommandDescriptions => s_commandDescriptions;

    public ChatCommandManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeCommandAsync(string category, string command, string[] args)
    {
        if (s_commandCategories.TryGetValue(category, out var commands))
        {
            if (commands.TryGetValue(command, out ChatCommandDelegate? commandDelegate))
                await commandDelegate(_serviceProvider, args);
        }
    }

    private static (ImmutableDictionary<string, ImmutableDictionary<string, ChatCommandDelegate>>, ImmutableArray<string>) RegisterCommands()
    {
        IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes()
                                  .Where(type => type.GetCustomAttribute<ChatCommandCategoryAttribute>() != null);

        MethodInfo getServiceMethod = typeof(ServiceProviderServiceExtensions).GetMethod(nameof(ServiceProviderServiceExtensions.GetRequiredService), [typeof(IServiceProvider)])!;
        var categories = ImmutableDictionary.CreateBuilder<string, ImmutableDictionary<string, ChatCommandDelegate>>();
        var descriptions = ImmutableArray.CreateBuilder<string>();

        foreach (Type type in types)
        {
            var commands = ImmutableDictionary.CreateBuilder<string, ChatCommandDelegate>();
            foreach (MethodInfo method in type.GetMethods())
            {
                ChatCommandAttribute? cmdAttribute = method.GetCustomAttribute<ChatCommandAttribute>();
                if (cmdAttribute == null) continue;

                ParameterExpression serviceProviderParam = Expression.Parameter(typeof(IServiceProvider));
                ParameterExpression argsParam = Expression.Parameter(typeof(string[]));

                MethodCallExpression getServiceCall = Expression.Call(getServiceMethod.MakeGenericMethod(type), serviceProviderParam);
                Expression handlerCall = Expression.Call(getServiceCall, method, argsParam);

                if (method.ReturnType == typeof(void)) // Allow non-async methods as well
                    handlerCall = Expression.Block(handlerCall, Expression.Constant(Task.CompletedTask));

                Expression<ChatCommandDelegate> lambda = Expression.Lambda<ChatCommandDelegate>(handlerCall, serviceProviderParam, argsParam);
                commands.Add(cmdAttribute.Command, lambda.Compile());

                ChatCommandDescAttribute? desc = method.GetCustomAttribute<ChatCommandDescAttribute>();
                if (desc != null)
                    descriptions.Add(desc.Description);
            }

            ChatCommandCategoryAttribute categoryAttribute = type.GetCustomAttribute<ChatCommandCategoryAttribute>()!;
            categories.Add(categoryAttribute.Category, commands.ToImmutable());
        }

        return (categories.ToImmutable(), descriptions.ToImmutable());
    }
}
