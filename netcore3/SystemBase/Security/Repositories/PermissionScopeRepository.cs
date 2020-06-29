using Sorschia.Repositories;
using Sorschia.Utilities;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;
using SystemBase.Security.Processes;

namespace SystemBase.Security.Repositories
{
    internal sealed class PermissionScopeRepository : RepositoryBase, IPermissionScopeRepository
    {
        public PermissionScopeRepository(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }

        public Task<bool> DeleteAsync(PermissionScope.DeleteModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IDeletePermissionScope>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<PermissionScope> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IGetPermissionScope>();
            process.Id = id;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<PermissionScope.SaveResult> SaveAsync(PermissionScope.SaveModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISavePermissionScope>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<PermissionScope.SearchResult> SearchAsync(PermissionScope.SearchModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISearchPermissionScope>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }
    }
}
