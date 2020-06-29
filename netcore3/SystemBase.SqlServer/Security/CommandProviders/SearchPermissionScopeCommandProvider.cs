using Sorschia.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.CommandProviders;
using SystemBase.Data;
using SystemBase.Security.Entities;
using SystemBase.Security.ReaderConverters;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class SearchPermissionScopeCommandProvider : PaginationCommandProviderBase
    {
        private const string PROCEDURE = "[Security].[SearchPermissionScope]";
        private const string PARAM_FILTERTEXT = "@FilterText";
        private const string PARAM_SKIPPEDIDS = "@SkippedIds";

        private readonly PermissionScopeReaderConverter _readerConverter;

        public SearchPermissionScopeCommandProvider(PermissionScopeReaderConverter readerConverter)
        {
            _readerConverter = readerConverter;
        }

        private SqlCommand Get(PermissionScope.SearchModel model, SqlConnection connection) =>
            connection.CreateProcedureCommand(PROCEDURE)
            .AddPaginationParameters(model)
            .AddInParameter(PARAM_FILTERTEXT, model.FilterText)
            .AddIntListParameter(PARAM_SKIPPEDIDS, model.SkippedIds);

        public async Task ExecuteAsync(PermissionScope.SearchModel model, PermissionScope.SearchResult result, SqlConnection connection, CancellationToken cancellationToken = default)
        {
            using var command = Get(model, connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (reader.HasRows)
                while (await reader.ReadAsync(cancellationToken))
                    result.Scopes.Add(_readerConverter.Convert(reader));

            result.TotalCount = await ReadTotalCountAsync(reader, cancellationToken);
        }
    }
}
