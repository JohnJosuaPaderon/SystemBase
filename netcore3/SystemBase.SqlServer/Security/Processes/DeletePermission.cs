using Sorschia.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class DeletePermission : ProcessBase, IDeletePermission
    {
        private readonly DeletePermissionCommandProvider _deletePermissionCommandProvider;

        public Permission.DeleteModel Model { get; set; }

        public DeletePermission(IConnectionStringProvider connectionStringProvider, DeletePermissionCommandProvider deletePermissionCommandProvider) : base(connectionStringProvider)
        {
            _deletePermissionCommandProvider = deletePermissionCommandProvider;
        }

        public async Task<bool> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var model = Model;
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                var result = await _deletePermissionCommandProvider.ExecuteAsync(model, connection, transaction, cancellationToken);
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
