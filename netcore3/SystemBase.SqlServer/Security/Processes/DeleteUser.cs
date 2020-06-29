using Sorschia.Data;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class DeleteUser : ProcessBase, IDeleteUser
    {
        private readonly DeleteUserCommandProvider _deleteUserCommandProvider;

        public User.DeleteModel Model { get; set; }

        public DeleteUser(IConnectionStringProvider connectionStringProvider, DeleteUserCommandProvider deleteUserCommandProvider) : base(connectionStringProvider)
        {
            _deleteUserCommandProvider = deleteUserCommandProvider;
        }

        public async Task<bool> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var model = Model;
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                var result = await _deleteUserCommandProvider.ExecuteAsync(model, connection, transaction, cancellationToken);
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
