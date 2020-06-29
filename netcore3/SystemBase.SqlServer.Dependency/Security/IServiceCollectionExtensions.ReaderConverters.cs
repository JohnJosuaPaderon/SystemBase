using Microsoft.Extensions.DependencyInjection;
using SystemBase.Security.ReaderConverters;

namespace SystemBase.Security
{
    partial class IServiceCollectionExtensions
    {
        private static IServiceCollection AddReaderConverters(this IServiceCollection instance) => instance
            .AddSingleton<ApplicationReaderConverter>()
            .AddSingleton<ModuleReaderConverter>()
            .AddSingleton<PermissionReaderConverter>()
            .AddSingleton<PermissionScopeReaderConverter>()
            .AddSingleton<PlatformReaderConverter>()
            .AddSingleton<UserReaderConverter>();
    }
}
