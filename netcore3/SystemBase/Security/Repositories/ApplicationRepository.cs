using Sorschia.Repositories;
using Sorschia.Utilities;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;
using SystemBase.Security.Processes;

namespace SystemBase.Security.Repositories
{
    internal sealed class ApplicationRepository : RepositoryBase, IApplicationRepository
    {
        public ApplicationRepository(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }

        public Task<bool> DeleteAsync(Application.DeleteModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IDeleteApplication>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Application> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IGetApplication>();
            process.Id = id;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Application.SaveResult> SaveAsync(Application.SaveModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISaveApplication>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Application.SearchResult> SearchAsync(Application.SearchModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISearchApplication>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }
    }
}
