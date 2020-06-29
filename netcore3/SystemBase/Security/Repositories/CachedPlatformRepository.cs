using Sorschia.Caching;
using Sorschia.Repositories;
using Sorschia.Utilities;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Configuration;
using SystemBase.Security.Entities;
using SystemBase.Security.Processes;

namespace SystemBase.Security.Repositories
{
    internal sealed class CachedPlatformRepository : CachedRepositoryBase, IPlatformRepository
    {
        private readonly SecurityConfiguration _configuration;

        public CachedPlatformRepository(IDependencyResolver dependencyResolver, ICache cache, ICacheHelper cacheHelper, SecurityConfiguration configuration) : base(dependencyResolver, cache, cacheHelper)
        {
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(Platform.DeleteModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Platform.DeleteModel, bool>(model);

            if (TryGetFromCache(cacheKey, out bool result))
                return result;

            using var process = GetProcess<IDeletePlatform>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Platform.Delete);
        }

        public async Task<Platform> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<int, Platform>(id);

            if (TryGetFromCache(cacheKey, out Platform platform))
                return platform;

            using var process = GetProcess<IGetPlatform>();
            process.Id = id;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Platform.Get);
        }

        public async Task<Platform.SaveResult> SaveAsync(Platform.SaveModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Platform.SaveModel, Platform.SaveResult>(model);

            if (TryGetFromCache(cacheKey, out Platform.SaveResult result))
                return result;

            using var process = GetProcess<ISavePlatform>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Platform.Save);
        }

        public async Task<Platform.SearchResult> SearchAsync(Platform.SearchModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Platform.SearchModel, Platform.SearchResult>(model);

            if (TryGetFromCache(cacheKey, out Platform.SearchResult result))
                return result;

            using var process = GetProcess<ISearchPlatform>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Platform.Search);
        }
    }
}
