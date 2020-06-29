using Sorschia.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Data;
using SystemBase.Security.Entities;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class SaveModuleCommandProvider
    {
        private const string PROCEDURE = "[Security].[SaveModule]";
        private const string PARAM_ID = "@Id";
        private const string PARAM_NAME = "@Name";
        private const string PARAM_ORDINALNUMBER = "@OrdinalNumber";
        private const string PARAM_APPLICATIONID = "@ApplicationId";
        private const SqlDbType PARAMTYPE_ID = SqlDbType.Int;
        private const int ALLOWEDAFFECTEDROWS = 1;

        private readonly ICurrentSessionProvider _sessionProvider;

        public SaveModuleCommandProvider(ICurrentSessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        private SqlCommand Get(Module module, SqlConnection connection, SqlTransaction transaction = default) =>
            connection.CreateProcedureCommand(PROCEDURE, transaction)
            .AddInOutParameter(PARAM_ID, module.Id, PARAMTYPE_ID)
            .AddInParameter(PARAM_NAME, module.Name)
            .AddInParameter(PARAM_ORDINALNUMBER, module.OrdinalNumber)
            .AddInParameter(PARAM_APPLICATIONID, module.ApplicationId)
            .AddSessionIdParameter(_sessionProvider);

        public async Task<Module> ExecuteAsync(Module module, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            using var command = Get(module, connection, transaction);

            if (await command.ExecuteNonQueryAsync(cancellationToken) == ALLOWEDAFFECTEDROWS)
            {
                module.Id = command.Parameters.GetInt32(PARAM_ID);
                return module;
            }

            return default;
        }
    }
}
