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
    internal sealed class SearchPermissionCommandProvider : PaginationCommandProviderBase
    {
        private const string PROCEDURE = "[Security].[SearchPermission]";
        private const string PARAM_FILTERTEXT = "@FilterText";
        private const string PARAM_FILTERBYSCOPE = "@FilterByScope";
        private const string PARAM_SCOPEIDS = "@ScopeIds";
        private const string PARAM_SKIPPEDIDS = "@SkippedIds";

        private readonly PermissionReaderConverter _readerConverter;

        public SearchPermissionCommandProvider(PermissionReaderConverter readerConverter)
        {
            _readerConverter = readerConverter;
        }

        private SqlCommand Get(Permission.SearchModel model, SqlConnection connection) =>
            connection.CreateProcedureCommand(PROCEDURE)
            .AddPaginationParameters(model)
            .AddInParameter(PARAM_FILTERTEXT, model.FilterText)
            .AddInParameter(PARAM_FILTERBYSCOPE, model.FilterByScope)
            .AddIntListParameter(PARAM_SCOPEIDS, model.ScopeIds)
            .AddIntListParameter(PARAM_SKIPPEDIDS, model.SkippedIds);

        public async Task ExecuteAsync(Permission.SearchModel model, Permission.SearchResult result, SqlConnection connection, CancellationToken cancellationToken = default)
        {
            using var command = Get(model, connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (reader.HasRows)
                while (await reader.ReadAsync(cancellationToken))
                    result.Permissions.Add(_readerConverter.Convert(reader));

            result.TotalCount = await ReadTotalCountAsync(reader, cancellationToken);
        }
    }
}
