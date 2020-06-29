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
    internal sealed class CachedModuleRepository : CachedRepositoryBase, IModuleRepository
    {
        private readonly SecurityConfiguration _configuration;

        public CachedModuleRepository(IDependencyResolver dependencyResolver, ICache cache, ICacheHelper cacheHelper, SecurityConfiguration configuration) : base(dependencyResolver, cache, cacheHelper)
        {
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(Module.DeleteModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Module.DeleteModel, bool>(model);

            if (TryGetFromCache(cacheKey, out bool result))
                return result;

            using var process = GetProcess<IDeleteModule>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Module.Delete);
        }

        public async Task<Module> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<int, Module>(id);

            if (TryGetFromCache(cacheKey, out Module module))
                return module;

            using var process = GetProcess<IGetModule>();
            process.Id = id;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Module.Get);
        }

        public async Task<Module.SaveResult> SaveAsync(Module.SaveModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Module.SaveModel, Module.SaveResult>(model);

            if (TryGetFromCache(cacheKey, out Module.SaveResult result))
                return result;

            using var process = GetProcess<ISaveModule>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Module.Save);
        }

        public async Task<Module.SearchResult> SearchAsync(Module.SearchModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<Module.SearchModel, Module.SearchResult>(model);

            if (TryGetFromCache(cacheKey, out Module.SearchResult result))
                return result;

            using var process = GetProcess<ISearchModule>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.Module.Search);
        }
    }
}
