using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Repositories;

namespace SystemBase.Security.Entities
{
    public static class ApplicationExtensions
    {
        public static async Task<Platform> GetPlatformAsync(this Application instance, IPlatformRepository repository, CancellationToken cancellationToken = default)
        {
            if (instance is null)
                return null;

            if (repository is null)
                throw SystemBaseException.RepositoryIsNull<IPlatformRepository>();

            var platformId = instance.PlatformId;

            if (platformId > 0)
            {
                var platform = await repository.GetAsync(platformId ?? 0, cancellationToken);
                instance.Platform = platform;
                return platform;
            }

            return null;
        }
    }
}
