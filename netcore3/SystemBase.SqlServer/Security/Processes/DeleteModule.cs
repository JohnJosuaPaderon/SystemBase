using Sorschia.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class DeleteModule : ProcessBase, IDeleteModule
    {
        private readonly DeleteModuleCommandProvider _deleteModuleCommandProvider;

        public Module.DeleteModel Model { get; set; }

        public DeleteModule(IConnectionStringProvider connectionStringProvider, DeleteModuleCommandProvider deleteModuleCommandProvider) : base(connectionStringProvider)
        {
            _deleteModuleCommandProvider = deleteModuleCommandProvider;
        }

        public async Task<bool> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var model = Model;
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                var result = await _deleteModuleCommandProvider.ExecuteAsync(model, connection, transaction, cancellationToken);
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
