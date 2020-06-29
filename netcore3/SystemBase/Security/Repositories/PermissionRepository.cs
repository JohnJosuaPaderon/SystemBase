using Sorschia.Repositories;
using Sorschia.Utilities;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;
using SystemBase.Security.Processes;

namespace SystemBase.Security.Repositories
{
    internal sealed class PermissionRepository : RepositoryBase, IPermissionRepository
    {
        public PermissionRepository(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }

        public Task<bool> DeleteAsync(Permission.DeleteModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IDeletePermission>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Permission> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<IGetPermission>();
            process.Id = id;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Permission.SaveResult> SaveAsync(Permission.SaveModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISavePermission>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<ApplicationPermission.SaveResult> SaveAsync(ApplicationPermission.SaveModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISaveApplicationPermission>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        } 

        public Task<ModulePermission.SaveResult> SaveAsync(ModulePermission.SaveModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISaveModulePermission>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }

        public Task<Permission.SearchResult> SearchAsync(Permission.SearchModel model, CancellationToken cancellationToken = default)
        {
            using var process = GetProcess<ISearchPermission>();
            process.Model = model;
            return process.ExecuteAsync(cancellationToken);
        }
    }
}
