using Sorschia.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Data;
using SystemBase.Security.Entities;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class DeleteModuleCommandProvider
    {
        private const string PROCEDURE = "[Security].[DeleteModule]";
        private const string PARAM_ID = "@Id";
        private const string PARAM_ISCASCADED = "@IsCascaded";
        private const int ALLOWEDAFFECTEDROWS = 1;

        private readonly ICurrentSessionProvider _sessionProvider;

        public DeleteModuleCommandProvider(ICurrentSessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        private SqlCommand Get(Module.DeleteModel model, SqlConnection connection, SqlTransaction transaction = default) =>
            connection.CreateProcedureCommand(PROCEDURE, transaction)
            .AddInParameter(PARAM_ID, model.Id)
            .AddInParameter(PARAM_ISCASCADED, model.IsCascaded)
            .AddSessionIdParameter(_sessionProvider);

        public async Task<bool> ExecuteAsync(Module.DeleteModel model, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            using var command = Get(model, connection, transaction);
            return await command.ExecuteNonQueryAsync(cancellationToken) == ALLOWEDAFFECTEDROWS;
        }
    }
}
