using Sorschia.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Data;
using SystemBase.Security.Entities;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class SaveModulePermissionCommandProvider
    {
        private const string PROCEDURE = "[Security].[SaveModulePermission]";
        private const string PARAM_ID = "@Id";
        private const string PARAM_NAME = "@Name";
        private const string PARAM_CODE = "@Code";
        private const string PARAM_MODULEID = "@ModuleId";
        private const SqlDbType PARAMTYPE_ID = SqlDbType.Int;
        private const int ALLOWEDAFFECTEDROWS = 1;

        private readonly ICurrentSessionProvider _sessionProvider;

        public SaveModulePermissionCommandProvider(ICurrentSessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        private SqlCommand Get(ModulePermission permission, SqlConnection connection, SqlTransaction transaction = default) =>
            connection.CreateProcedureCommand(PROCEDURE, transaction)
            .AddInOutParameter(PARAM_ID, permission.Id, PARAMTYPE_ID)
            .AddInParameter(PARAM_NAME, permission.Name)
            .AddInParameter(PARAM_CODE, permission.Code)
            .AddInParameter(PARAM_MODULEID, permission.ModuleId)
            .AddSessionIdParameter(_sessionProvider);

        public async Task<ModulePermission> ExecuteAsync(ModulePermission permission, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            using var command = Get(permission, connection, transaction);

            if (await command.ExecuteNonQueryAsync(cancellationToken) == ALLOWEDAFFECTEDROWS)
            {
                permission.Id = command.Parameters.GetInt32(PARAM_ID);
                return permission;
            }

            return default;
        }
    }
}
