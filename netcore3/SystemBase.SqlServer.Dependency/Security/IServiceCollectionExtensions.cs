using Microsoft.Extensions.DependencyInjection;

namespace SystemBase.Security
{
    internal static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection instance) => instance
            .AddCommandProviders()
            .AddProcesses()
            .AddReaderConverters();
    }
}
