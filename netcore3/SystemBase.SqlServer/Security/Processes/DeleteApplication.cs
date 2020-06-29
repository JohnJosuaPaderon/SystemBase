using Sorschia.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class DeleteApplication : ProcessBase, IDeleteApplication
    {
        private readonly DeleteApplicationCommandProvider _deleteApplicationCommandProvider;

        public Application.DeleteModel Model { get; set; }

        public DeleteApplication(IConnectionStringProvider connectionStringProvider, DeleteApplicationCommandProvider deleteApplicationCommandProvider) : base(connectionStringProvider)
        {
            _deleteApplicationCommandProvider = deleteApplicationCommandProvider;
        }
        
        public async Task<bool> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var model = Model;
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                var result = await _deleteApplicationCommandProvider.ExecuteAsync(model, connection, transaction, cancellationToken);
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
