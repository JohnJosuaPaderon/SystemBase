using Sorschia.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Data;
using SystemBase.Security.Entities;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class SaveApplicationCommandProvider
    {
        private const string PROCEDURE = "[Security].[SaveApplication]";
        private const string PARAM_ID = "@Id";
        private const string PARAM_NAME = "@Name";
        private const string PARAM_PLATFORMID = "@PlatformId";
        private const SqlDbType PARAMTYPE_ID = SqlDbType.Int;
        private const int ALLOWEDAFFECTEDROWS = 1;

        private readonly ICurrentSessionProvider _sessionProvider;

        public SaveApplicationCommandProvider(ICurrentSessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        private SqlCommand Get(Application application, SqlConnection connection, SqlTransaction transaction = default) =>
            connection.CreateProcedureCommand(PROCEDURE, transaction)
            .AddInOutParameter(PARAM_ID, application.Id, PARAMTYPE_ID)
            .AddInParameter(PARAM_NAME, application.Name)
            .AddInParameter(PARAM_PLATFORMID, application.PlatformId)
            .AddSessionIdParameter(_sessionProvider);

        public async Task<Application> ExecuteAsync(Application application, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            using var command = Get(application, connection, transaction);

            if (await command.ExecuteNonQueryAsync(cancellationToken) == ALLOWEDAFFECTEDROWS)
            {
                application.Id = command.Parameters.GetInt32(PARAM_ID);
                return application;
            }

            return default;
        }
    }
}
