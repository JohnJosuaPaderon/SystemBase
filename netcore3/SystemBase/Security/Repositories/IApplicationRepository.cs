using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Repositories
{
    public interface IApplicationRepository
    {
        Task<bool> DeleteAsync(Application.DeleteModel model, CancellationToken cancellationToken = default);
        Task<Application> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Application.SaveResult> SaveAsync(Application.SaveModel model, CancellationToken cancellationToken = default);
        Task<Application.SearchResult> SearchAsync(Application.SearchModel model, CancellationToken cancellationToken = default);
    }
}
