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
    internal sealed class CachedApplicationRepository : CachedRepositoryBase, IApplicationRepository
    {
        private readonly SecurityConfiguration _configuration;

        public CachedApplicationRepository(IDependencyResolver dependencyResolver, ICache cache, ICacheHelper cacheHelper, SecurityConfiguration configuration) : base(dependencyResolver, cache, cacheHelper)
        {
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(Application.DeleteModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Application.DeleteModel, bool>(model);

            if (TryGetFromCache(cacheKey, out bool result))
                return result;

            using var process = GetProcess<IDeleteApplication>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Application.Delete);
        }

        public async Task<Application> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<int, Application>(id);

            if (TryGetFromCache(cacheKey, out Application application))
                return application;

            using var process = GetProcess<IGetApplication>();
            process.Id = id;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Application.Get);
        }

        public async Task<Application.SaveResult> SaveAsync(Application.SaveModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Application.SaveModel, Application.SaveResult>(model);

            if (TryGetFromCache(cacheKey, out Application.SaveResult result))
                return result;

            using var process = GetProcess<ISaveApplication>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Application.Save);
        }

        public async Task<Application.SearchResult> SearchAsync(Application.SearchModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Application.SearchModel, Application.SearchResult>(model);

            if (TryGetFromCache(cacheKey, out Application.SearchResult result))
                return result;

            using var process = GetProcess<ISearchApplication>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Application.Search);
        }
    }
}
