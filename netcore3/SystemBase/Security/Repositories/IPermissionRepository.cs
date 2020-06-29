using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Repositories
{
    public interface IPermissionRepository
    {
        Task<bool> DeleteAsync(Permission.DeleteModel model, CancellationToken cancellationToken = default);
        Task<Permission> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Permission.SaveResult> SaveAsync(Permission.SaveModel model, CancellationToken cancellationToken = default);
        Task<ApplicationPermission.SaveResult> SaveAsync(ApplicationPermission.SaveModel model, CancellationToken cancellationToken = default);
        Task<ModulePermission.SaveResult> SaveAsync(ModulePermission.SaveModel model, CancellationToken cancellationToken = default);
        Task<Permission.SearchResult> SearchAsync(Permission.SearchModel model, CancellationToken cancellationToken = default);
    }
}
