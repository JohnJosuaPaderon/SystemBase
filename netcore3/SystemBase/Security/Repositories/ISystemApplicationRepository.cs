using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Repositories
{
    public interface ISystemApplicationRepository
    {
        Task<bool> DeleteAsync(SystemApplication.DeleteModel model, CancellationToken cancellationToken = default);
        Task<SystemApplication> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<SystemApplication.SearchResult> SearchAsync(SystemApplication.SearchModel model, CancellationToken cancellationToken = default);
        Task<SystemApplication.SaveResult> SaveAsync(SystemApplication.SaveModel model, CancellationToken cancellationToken = default);
    }
}
