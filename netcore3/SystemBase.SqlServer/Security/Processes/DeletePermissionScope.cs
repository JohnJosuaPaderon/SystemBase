using Sorschia.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class DeletePermissionScope : ProcessBase, IDeletePermissionScope
    {
        private readonly DeletePermissionScopeCommandProvider _deletePermissionScopeCommandProvider;

        public PermissionScope.DeleteModel Model { get; set; }
        
        public DeletePermissionScope(IConnectionStringProvider connectionStringProvider, DeletePermissionScopeCommandProvider deletePermissionScopeCommandProvider) : base(connectionStringProvider)
        {
            _deletePermissionScopeCommandProvider = deletePermissionScopeCommandProvider;
        }

        public async Task<bool> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var model = Model;
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                var result = await _deletePermissionScopeCommandProvider.ExecuteAsync(model, connection, transaction, cancellationToken);
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                ThrowError(ex);
                return default;
            }
        }
    }
}
