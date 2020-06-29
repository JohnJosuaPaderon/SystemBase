using Sorschia.Data;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Data;
using SystemBase.Security.Entities;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class SaveUserApplicationCommandProvider
    {
        private const string PROCEDURE = "[Security].[SaveUserApplication]";
        private const string PARAM_ID = "@Id";
        private const string PARAM_USERID = "@UserId";
        private const string PARAM_APPLICATIONID = "@ApplicationId";
        private const string PARAM_ISAPPROVED = "@IsApproved";
        private const SqlDbType PARAMTYPE_ID = SqlDbType.BigInt;
        private const int ALLOWEDAFFECTEDROWS = 1;

        private readonly ICurrentSessionProvider _sessionProvider;

        public SaveUserApplicationCommandProvider(ICurrentSessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        private SqlCommand Get(UserApplication userApplication, SqlConnection connection, SqlTransaction transaction = default) =>
            connection.CreateProcedureCommand(PROCEDURE, transaction)
            .AddInOutParameter(PARAM_ID, userApplication.Id, PARAMTYPE_ID)
            .AddInParameter(PARAM_USERID, userApplication.UserId)
            .AddInParameter(PARAM_APPLICATIONID, userApplication.ApplicationId)
            .AddInParameter(PARAM_ISAPPROVED, userApplication.IsApproved)
            .AddSessionIdParameter(_sessionProvider);

        public async Task<UserApplication> ExecuteAsync(UserApplication userApplication, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            using var command = Get(userApplication, connection, transaction);

            if (await command.ExecuteNonQueryAsync(cancellationToken) == ALLOWEDAFFECTEDROWS)
            {
                userApplication.Id = command.Parameters.GetInt64(PARAM_ID);
                return userApplication;
            }

            return default;
        }
    }
}
