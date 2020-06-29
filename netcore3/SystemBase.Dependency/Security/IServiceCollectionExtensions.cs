using Microsoft.Extensions.DependencyInjection;
using SystemBase.Security.Repositories;

namespace SystemBase.Security
{
    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection instance, SecurityDependencySettings dependencySettings)
        {
            if (dependencySettings == null)
                SystemBaseException.DependencySettingsIsNull<SecurityDependencySettings>();

            if (dependencySettings.UseRepositoryCaching)
                instance
                    .AddSingleton<IApplicationRepository, CachedApplicationRepository>()
                    .AddSingleton<IModuleRepository, CachedModuleRepository>()
                    .AddSingleton<IPermissionRepository, CachedPermissionRepository>()
                    .AddSingleton<IPermissionScopeRepository, CachedPermissionScopeRepository>()
                    .AddSingleton<IPlatformRepository, CachedPlatformRepository>()
                    .AddSingleton<IUserRepository, CachedUserRepository>();
            else
                instance
                    .AddSingleton<IApplicationRepository, ApplicationRepository>()
                    .AddSingleton<IModuleRepository, ModuleRepository>()
                    .AddSingleton<IPermissionRepository, PermissionRepository>()
                    .AddSingleton<IPermissionScopeRepository, PermissionScopeRepository>()
                    .AddSingleton<IPlatformRepository, PlatformRepository>()
                    .AddSingleton<IUserRepository, UserRepository>();

            return instance;
        }
    }
}
