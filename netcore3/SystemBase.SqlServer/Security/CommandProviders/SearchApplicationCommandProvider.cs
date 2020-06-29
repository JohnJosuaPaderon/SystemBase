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
    internal sealed class SearchApplicationCommandProvider : PaginationCommandProviderBase
    {
        private const string PROCEDURE = "[Security].[SearchApplication]";
        private const string PARAM_FILTERTEXT = "@FilterText";
        private const string PARAM_FILTERBYPLATFORM = "@FilterByPlatform";
        private const string PARAM_PLATFORMIDS = "@PlatformIds";
        private const string PARAM_SKIPPEDIDS = "@SkippedIds";

        private readonly ApplicationReaderConverter _readerConverter;

        public SearchApplicationCommandProvider(ApplicationReaderConverter readerConverter)
        {
            _readerConverter = readerConverter;
        }

        private SqlCommand Get(Application.SearchModel model, SqlConnection connection) =>
            connection.CreateProcedureCommand(PROCEDURE)
            .AddPaginationParameters(model)
            .AddInParameter(PARAM_FILTERTEXT, model.FilterText)
            .AddInParameter(PARAM_FILTERBYPLATFORM, model.FilterByPlatform)
            .AddIntListParameter(PARAM_PLATFORMIDS, model.PlatformIds)
            .AddIntListParameter(PARAM_SKIPPEDIDS, model.SkippedIds);

        public async Task ExecuteAsync(Application.SearchModel model, Application.SearchResult result, SqlConnection connection, CancellationToken cancellationToken = default)
        {
            using var command = Get(model, connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (reader.HasRows)
                while (await reader.ReadAsync(cancellationToken))
                    result.Applications.Add(_readerConverter.Convert(reader));

            result.TotalCount = await ReadTotalCountAsync(reader, cancellationToken);
        }
    }
}
