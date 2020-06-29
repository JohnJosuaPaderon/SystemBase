using Sorschia.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class DeletePlatform : ProcessBase, IDeletePlatform
    {
        private readonly DeletePlatformCommandProvider _deletePlatformCommandProvider;

        public Platform.DeleteModel Model { get; set; }

        public DeletePlatform(IConnectionStringProvider connectionStringProvider, DeletePlatformCommandProvider deletePlatformCommandProvider) : base(connectionStringProvider)
        {
            _deletePlatformCommandProvider = deletePlatformCommandProvider;
        }

        public async Task<bool> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var model = Model;
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                var result = await _deletePlatformCommandProvider.ExecuteAsync(model, connection, transaction, cancellationToken);
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
