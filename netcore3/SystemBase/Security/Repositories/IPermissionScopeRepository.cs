using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Repositories
{
    public interface IPermissionScopeRepository
    {
        Task<bool> DeleteAsync(PermissionScope.DeleteModel model, CancellationToken cancellationToken = default);
        Task<PermissionScope> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<PermissionScope.SaveResult> SaveAsync(PermissionScope.SaveModel model, CancellationToken cancellationToken = default);
        Task<PermissionScope.SearchResult> SearchAsync(PermissionScope.SearchModel model, CancellationToken cancellationToken = default);
    }
}
