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
    internal sealed class CachedPermissionRepository : CachedRepositoryBase, IPermissionRepository
    {
        private readonly SecurityConfiguration _configuration;

        public CachedPermissionRepository(IDependencyResolver dependencyResolver, ICache cache, ICacheHelper cacheHelper, SecurityConfiguration configuration) : base(dependencyResolver, cache, cacheHelper)
        {
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(Permission.DeleteModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Permission.DeleteModel, bool>(model);

            if (TryGetFromCache(cacheKey, out bool result))
                return result;

            using var process = GetProcess<IDeletePermission>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Permission.Delete);
        }

        public async Task<Permission> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<int, Permission>(id);

            if (TryGetFromCache(cacheKey, out Permission permission))
                return permission;

            using var process = GetProcess<IGetPermission>();
            process.Id = id;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Permission.Get);
        }

        public async Task<Permission.SaveResult> SaveAsync(Permission.SaveModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Permission.SaveModel, Permission.SaveResult>(model);

            if (TryGetFromCache(cacheKey, out Permission.SaveResult result))
                return result;

            using var process = GetProcess<ISavePermission>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Permission.Save);
        }

        public async Task<ApplicationPermission.SaveResult> SaveAsync(ApplicationPermission.SaveModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<ApplicationPermission.SaveModel, ApplicationPermission.SaveResult>(model);

            if (TryGetFromCache(cacheKey, out ApplicationPermission.SaveResult result))
                return result;

            using var process = GetProcess<ISaveApplicationPermission>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Permission.Save);

        }

        public async Task<ModulePermission.SaveResult> SaveAsync(ModulePermission.SaveModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<ModulePermission.SaveModel, ModulePermission.SaveResult>(model);

            if (TryGetFromCache(cacheKey, out ModulePermission.SaveResult result))
                return result;

            using var process = GetProcess<ISaveModulePermission>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Permission.Save);
        }

        public async Task<Permission.SearchResult> SearchAsync(Permission.SearchModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Permission.SearchModel, Permission.SearchResult>(model);

            if (TryGetFromCache(cacheKey, out Permission.SearchResult result))
                return result;

            using var process = GetProcess<ISearchPermission>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Permission.Search);
        }
    }
}
