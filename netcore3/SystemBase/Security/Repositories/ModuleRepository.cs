using Sorschia.Repositories;
using Sorschia.Utilities;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;
using SystemBase.Security.Processes;

namespace SystemBase.Security.Repositories
{
    internal sealed class ModuleRepository : RepositoryBase, IModuleRepository
    {
        public ModuleRepository(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }

        public Task<bool> DeleteAsync(Module.DeleteModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IDeleteModule>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Module> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IGetModule>();
            process.Id = id;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Module.SaveResult> SaveAsync(Module.SaveModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISaveModule>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Module.SearchResult> SearchAsync(Module.SearchModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISearchModule>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }
    }
}
