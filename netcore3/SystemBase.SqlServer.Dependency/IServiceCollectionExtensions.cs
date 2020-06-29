using Microsoft.Extensions.DependencyInjection;
using SystemBase.Security;

namespace SystemBase
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSystemBaseSqlServer(this IServiceCollection instance) => instance
            .AddSecurity();
    }
}
