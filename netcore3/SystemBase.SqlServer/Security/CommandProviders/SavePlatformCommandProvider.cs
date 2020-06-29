using Sorschia.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Data;
using SystemBase.Security.Entities;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class SavePlatformCommandProvider
    {
        private const string PROCEDURE = "[Security].[SavePlatform]";
        private const string PARAM_ID = "@Id";
        private const string PARAM_NAME = "@Name";
        private const SqlDbType PARAMTYPE_ID = SqlDbType.Int;
        private const int ALLOWEDAFFECTEDROWS = 1;

        private readonly ICurrentSessionProvider _sessionProvider;

        public SavePlatformCommandProvider(ICurrentSessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        private SqlCommand Get(Platform platform, SqlConnection connection, SqlTransaction transaction = default) =>
            connection.CreateProcedureCommand(PROCEDURE, transaction)
            .AddInOutParameter(PARAM_ID, platform.Id, PARAMTYPE_ID)
            .AddInParameter(PARAM_NAME, platform.Name)
            .AddSessionIdParameter(_sessionProvider);

        public async Task<Platform> ExecuteAsync(Platform platform, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            using var command = Get(platform, connection, transaction);

            if (await command.ExecuteNonQueryAsync(cancellationToken) == ALLOWEDAFFECTEDROWS)
            {
                platform.Id = command.Parameters.GetInt32(PARAM_ID);
                return platform;
            }

            return default;
        }
    }
}
