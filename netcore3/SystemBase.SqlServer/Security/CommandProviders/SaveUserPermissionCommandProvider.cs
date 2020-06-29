using Sorschia.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Data;
using SystemBase.Security.Entities;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class SaveUserPermissionCommandProvider
    {
        private const string PROCEDURE = "[Security].[SaveUserPermission]";
        private const string PARAM_ID = "@Id";
        private const string PARAM_USERID = "@UserId";
        private const string PARAM_PERMISSIONID = "@PermissionId";
        private const string PARAM_ISAPPROVED = "@IsApproved";
        private const SqlDbType PARAMTYPE_ID = SqlDbType.BigInt;
        private const int ALLOWEDAFFECTEDROWS = 1;

        private readonly ICurrentSessionProvider _sessionProvider;

        public SaveUserPermissionCommandProvider(ICurrentSessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        private SqlCommand Get(UserPermission userPermission, SqlConnection connection, SqlTransaction transaction = default) =>
            connection.CreateProcedureCommand(PROCEDURE, transaction)
            .AddInOutParameter(PARAM_ID, userPermission.Id, PARAMTYPE_ID)
            .AddInParameter(PARAM_USERID, userPermission.UserId)
            .AddInParameter(PARAM_PERMISSIONID, userPermission.PermissionId)
            .AddInParameter(PARAM_ISAPPROVED, userPermission.IsApproved)
            .AddSessionIdParameter(_sessionProvider);

        public async Task<UserPermission> ExecuteAsync(UserPermission userPermission, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            using var command = Get(userPermission, connection, transaction);

            if (await command.ExecuteNonQueryAsync(cancellationToken) == ALLOWEDAFFECTEDROWS)
            {
                userPermission.Id = command.Parameters.GetInt64(PARAM_ID);
                return userPermission;
            }

            return default;
        }
    }
}
