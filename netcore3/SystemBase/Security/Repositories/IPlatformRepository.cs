using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Repositories
{
    public interface IPlatformRepository
    {
        Task<bool> DeleteAsync(Platform.DeleteModel model, CancellationToken cancellationToken = default);
        Task<Platform> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Platform.SaveResult> SaveAsync(Platform.SaveModel model, CancellationToken cancellationToken = default);
        Task<Platform.SearchResult> SearchAsync(Platform.SearchModel model, CancellationToken cancellationToken = default);
    }
}
