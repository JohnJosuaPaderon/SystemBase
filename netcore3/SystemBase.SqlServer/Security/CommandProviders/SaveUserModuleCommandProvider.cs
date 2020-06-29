using Sorschia.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Data;
using SystemBase.Security.Entities;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class SaveUserModuleCommandProvider
    {
        private const string PROCEDURE = "[Security].[SaveUserModule]";
        private const string PARAM_ID = "@Id";
        private const string PARAM_USERID = "@UserId";
        private const string PARAM_MODULEID = "@ModuleId";
        private const string PARAM_ISAPPROVED = "@IsApproved";
        private const SqlDbType PARAMTYPE_ID = SqlDbType.BigInt;
        private const int ALLOWEDAFFECTEDROWS = 1;

        private readonly ICurrentSessionProvider _sessionProvider;

        public SaveUserModuleCommandProvider(ICurrentSessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        private SqlCommand Get(UserModule userModule, SqlConnection connection, SqlTransaction transaction = default) =>
            connection.CreateProcedureCommand(PROCEDURE, transaction)
            .AddInOutParameter(PARAM_ID, userModule.Id, PARAMTYPE_ID)
            .AddInParameter(PARAM_USERID, userModule.UserId)
            .AddInParameter(PARAM_MODULEID, userModule.ModuleId)
            .AddInParameter(PARAM_ISAPPROVED, userModule.IsApproved)
            .AddSessionIdParameter(_sessionProvider);

        public async Task<UserModule> ExecuteAsync(UserModule userModule, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            using var command = Get(userModule, connection, transaction);

            if (await command.ExecuteNonQueryAsync(cancellationToken) == ALLOWEDAFFECTEDROWS)
            {
                userModule.Id = command.Parameters.GetInt64(PARAM_ID);
                return userModule;
            }

            return default;
        }
    }
}
