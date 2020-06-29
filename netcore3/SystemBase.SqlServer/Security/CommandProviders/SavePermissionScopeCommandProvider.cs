using Sorschia.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Data;
using SystemBase.Security.Entities;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class SavePermissionScopeCommandProvider
    {
        private const string PROCEDURE = "[Security].[SavePermissionScope]";
        private const string PARAM_ID = "@Id";
        private const string PARAM_NAME = "@Name";
        private const SqlDbType PARAMTYPE_ID = SqlDbType.Int;
        private const int ALLOWEDAFFECTEDROWS = 1;

        private readonly ICurrentSessionProvider _sessionProvider;

        public SavePermissionScopeCommandProvider(ICurrentSessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        private SqlCommand Get(PermissionScope scope, SqlConnection connection, SqlTransaction transaction = default) =>
            connection.CreateProcedureCommand(PROCEDURE, transaction)
            .AddInOutParameter(PARAM_ID, scope.Id, PARAMTYPE_ID)
            .AddInParameter(PARAM_NAME, scope.Name)
            .AddSessionIdParameter(_sessionProvider);

        public async Task<PermissionScope> ExecuteAsync(PermissionScope scope, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            using var command = Get(scope, connection, transaction);

            if (await command.ExecuteNonQueryAsync(cancellationToken) == ALLOWEDAFFECTEDROWS)
            {
                scope.Id = command.Parameters.GetInt32(PARAM_ID);
                return scope;
            }

            return default;
        }
    }
}
