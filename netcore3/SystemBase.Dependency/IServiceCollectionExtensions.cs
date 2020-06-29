using Microsoft.Extensions.DependencyInjection;
using System;
using SystemBase.Security;

namespace SystemBase
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSystemBase(this IServiceCollection instance, Action<DependencySettings> configureSettings = null)
        {
            var dependencySettings = new DependencySettings();

            configureSettings?.Invoke(dependencySettings);

            return instance
                .AddSecurity(dependencySettings.Security);
        }
    }
}
