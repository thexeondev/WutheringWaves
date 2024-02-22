using Core.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseLocalResources(this IServiceCollection services)
    {
        return services.AddSingleton<IResourceProvider, LocalResourceProvider>();
    }
}
