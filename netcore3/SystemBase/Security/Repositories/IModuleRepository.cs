using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Repositories
{
    public interface IModuleRepository
    {
        Task<bool> DeleteAsync(Module.DeleteModel model, CancellationToken cancellationToken = default);
        Task<Module> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Module.SaveResult> SaveAsync(Module.SaveModel model, CancellationToken cancellationToken = default);
        Task<Module.SearchResult> SearchAsync(Module.SearchModel model, CancellationToken cancellationToken = default);
    }
}
