using Microsoft.Extensions.DependencyInjection;
using SystemBase.Security.CommandProviders;

namespace SystemBase.Security
{
    partial class IServiceCollectionExtensions
    {
        private static IServiceCollection AddCommandProviders(this IServiceCollection instance) => instance
            .AddSingleton<DeleteApplicationCommandProvider>()
            .AddSingleton<DeleteModuleCommandProvider>()
            .AddSingleton<DeletePermissionCommandProvider>()
            .AddSingleton<DeletePermissionScopeCommandProvider>()
            .AddSingleton<DeletePlatformCommandProvider>()
            .AddSingleton<DeleteUserApplicationCommandProvider>()
            .AddSingleton<DeleteUserCommandProvider>()
            .AddSingleton<DeleteUserModuleCommandProvider>()
            .AddSingleton<DeleteUserPermissionCommandProvider>()
            .AddSingleton<GetApplicationCommandProvider>()
            .AddSingleton<GetModuleCommandProvider>()
            .AddSingleton<GetPermissionCommandProvider>()
            .AddSingleton<GetPermissionScopeCommandProvider>()
            .AddSingleton<GetPlatformCommandProvider>()
            .AddSingleton<GetUserCommandProvider>()
            .AddSingleton<SaveApplicationCommandProvider>()
            .AddSingleton<SaveApplicationPermissionCommandProvider>()
            .AddSingleton<SaveModuleCommandProvider>()
            .AddSingleton<SaveModulePermissionCommandProvider>()
            .AddSingleton<SavePermissionCommandProvider>()
            .AddSingleton<SavePermissionScopeCommandProvider>()
            .AddSingleton<SavePlatformCommandProvider>()
            .AddSingleton<SaveUserApplicationCommandProvider>()
            .AddSingleton<SaveUserCommandProvider>()
            .AddSingleton<SaveUserModuleCommandProvider>()
            .AddSingleton<SaveUserPermissionCommandProvider>();
    }
}
