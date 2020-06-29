using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Repositories
{
    public interface IUserRepository
    {
        Task<bool> DeleteAsync(User.DeleteModel model, CancellationToken cancellationToken = default);
        Task<User> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<User.SaveResult> SaveAsync(User.SaveModel model, CancellationToken cancellationToken = default);
        Task<User.SearchResult> SearchAsync(User.SearchModel model, CancellationToken cancellationToken = default);
    }
}
