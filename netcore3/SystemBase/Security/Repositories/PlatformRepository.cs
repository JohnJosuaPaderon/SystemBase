using Sorschia.Repositories;
using Sorschia.Utilities;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;
using SystemBase.Security.Processes;

namespace SystemBase.Security.Repositories
{
    internal sealed class PlatformRepository : RepositoryBase, IPlatformRepository
    {
        public PlatformRepository(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }

        public Task<bool> DeleteAsync(Platform.DeleteModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IDeletePlatform>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Platform> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IGetPlatform>();
            process.Id = id;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Platform.SaveResult> SaveAsync(Platform.SaveModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISavePlatform>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Platform.SearchResult> SearchAsync(Platform.SearchModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISearchPlatform>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }
    }
}
