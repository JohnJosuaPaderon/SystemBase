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
    internal sealed class CachedPermissionScopeRepository : CachedRepositoryBase, IPermissionScopeRepository
    {
        private readonly SecurityConfiguration _configuration;

        public CachedPermissionScopeRepository(IDependencyResolver dependencyResolver, ICache cache, ICacheHelper cacheHelper, SecurityConfiguration configuration) : base(dependencyResolver, cache, cacheHelper)
        {
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(PermissionScope.DeleteModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<PermissionScope.DeleteModel, bool>(model);

            if (TryGetFromCache(cacheKey, out bool result))
                return result;

            using var process = GetProcess<IDeletePermissionScope>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.PermissionScope.Delete);
        }

        public async Task<PermissionScope> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<int, PermissionScope>(id);

            if (TryGetFromCache(cacheKey, out PermissionScope permissionScope))
                return permissionScope;

            using var process = GetProcess<IGetPermissionScope>();
            process.Id = id;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.PermissionScope.Get);
        }

        public async Task<PermissionScope.SaveResult> SaveAsync(PermissionScope.SaveModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<PermissionScope.SaveModel, PermissionScope.SaveResult>(model);

            if (TryGetFromCache(cacheKey, out PermissionScope.SaveResult result))
                return result;

            using var process = GetProcess<ISavePermissionScope>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.PermissionScope.Save);
        }

        public async Task<PermissionScope.SearchResult> SearchAsync(PermissionScope.SearchModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<PermissionScope.SearchModel, PermissionScope.SearchResult>(model);

            if (TryGetFromCache(cacheKey, out PermissionScope.SearchResult result))
                return result;

            using var process = GetProcess<ISearchPermissionScope>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.PermissionScope.Search);
        }
    }
}
