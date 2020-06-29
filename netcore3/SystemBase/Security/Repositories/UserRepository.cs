using Sorschia.Repositories;
using Sorschia.Utilities;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;
using SystemBase.Security.Processes;

namespace SystemBase.Security.Repositories
{
    internal sealed class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }

        public Task<bool> DeleteAsync(User.DeleteModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IDeleteUser>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<User> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IGetUser>();
            process.Id = id;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<User.SaveResult> SaveAsync(User.SaveModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISaveUser>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<User.SearchResult> SearchAsync(User.SearchModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISearchUser>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }
    }
}
