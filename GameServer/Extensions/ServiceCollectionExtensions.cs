using System.Reflection;
using GameServer.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Extensions;
internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        IEnumerable<Type> handlerTypes = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.IsAssignableTo(typeof(MessageHandlerBase)) && !t.IsAbstract);

        foreach (Type type in handlerTypes)
        {
            services.AddScoped(type);
        }

        return services;
    }
}
