using System.Reflection;
using GameServer.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Extensions;
internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddControllers(this IServiceCollection services)
    {
        IEnumerable<Type> controllerTypes = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.IsAssignableTo(typeof(Controller)) && !t.IsAbstract);

        foreach (Type type in controllerTypes)
        {
            services.AddScoped(type);
        }

        return services;
    }
}
