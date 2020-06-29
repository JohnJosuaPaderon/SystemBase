using Sorschia.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Data;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class DeleteUserApplicationCommandProvider
    {
        private const string PROCEDURE = "[Security].[DeleteUserApplication]";
        private const string PARAM_ID = "@Id";
        private const int ALLOWEDAFFECTEDROWS = 1;

        private readonly ICurrentSessionProvider _sessionProvider;

        public DeleteUserApplicationCommandProvider(ICurrentSessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        private SqlCommand Get(long id, SqlConnection connection, SqlTransaction transaction = default) =>
            connection.CreateProcedureCommand(PROCEDURE, transaction)
            .AddInParameter(PARAM_ID, id)
            .AddSessionIdParameter(_sessionProvider);

        public async Task<bool> ExecuteAsync(long id, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            using var command = Get(id, connection, transaction);
            return await command.ExecuteNonQueryAsync(cancellationToken) == ALLOWEDAFFECTEDROWS;
        }
    }
}
