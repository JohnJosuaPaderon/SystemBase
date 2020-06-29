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
    internal sealed class CachedUserRepository : CachedRepositoryBase, IUserRepository
    {
        private readonly SecurityConfiguration _configuration;

        public CachedUserRepository(IDependencyResolver dependencyResolver, ICache cache, ICacheHelper cacheHelper, SecurityConfiguration configuration) : base(dependencyResolver, cache, cacheHelper)
        {
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(User.DeleteModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<User.DeleteModel, bool>(model);

            if (TryGetFromCache(cacheKey, out bool result))
                return result;

            using var process = GetProcess<IDeleteUser>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.User.Delete);
        }

        public async Task<User> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<int, User>(id);

            if (TryGetFromCache(cacheKey, out User user))
                return user;

            using var process = GetProcess<IGetUser>();
            process.Id = id;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.User.Get);
        }

        public async Task<User.SaveResult> SaveAsync(User.SaveModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<User.SaveModel, User.SaveResult>(model);

            if (TryGetFromCache(cacheKey, out User.SaveResult result))
                return result;

            using var process = GetProcess<ISaveUser>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.User.Save);
        }

        public async Task<User.SearchResult> SearchAsync(User.SearchModel model, CancellationToken cancellationToken = default)
        {
            var cacheKey = _cacheHelper.CreateKey<User.SearchModel, User.SearchResult>(model);

            if (TryGetFromCache(cacheKey, out User.SearchResult result))
                return result;

            using var process = GetProcess<ISearchUser>();
            process.Model = model;
            return TrySaveToCache(cacheKey, await process.ExecuteAsync(cancellationToken), _configuration.CacheExpiration.User.Search);
        }
    }
}
