using Sorschia.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Entities;
using SystemBase.Security.ReaderConverters;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class GetPermissionScopeCommandProvider
    {
        private const string PROCEDURE = "[Security].[GetPermissionScope]";
        private const string PARAM_ID = "@Id";

        private readonly PermissionScopeReaderConverter _readerConverter;

        public GetPermissionScopeCommandProvider(PermissionScopeReaderConverter readerConverter)
        {
            _readerConverter = readerConverter;
        }

        private SqlCommand Get(int id, SqlConnection connection) =>
            connection.CreateProcedureCommand(PROCEDURE)
            .AddInParameter(PARAM_ID, id);

        public async Task<PermissionScope> ExecuteAsync(int id, SqlConnection connection, CancellationToken cancellationToken = default)
        {
            using var command = Get(id, connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (reader.HasRows && await reader.ReadAsync(cancellationToken))
                return _readerConverter.Convert(reader);

            return default;
        }
    }
}
