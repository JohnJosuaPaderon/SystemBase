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
    internal sealed class SearchModuleCommandProvider : PaginationCommandProviderBase
    {
        private const string PROCEDURE = "[Security].[SearchModule]";
        private const string PARAM_FILTERTEXT = "@FilterText";
        private const string PARAM_FILTERBYAPPLICATION = "@FilterByApplication";
        private const string PARAM_APPLICATIONIDS = "@ApplicationIds";
        private const string PARAM_SKIPPEDIDS = "@SkippedIds";

        private readonly ModuleReaderConverter _readerConverter;

        public SearchModuleCommandProvider(ModuleReaderConverter readerConverter)
        {
            _readerConverter = readerConverter;
        }

        private SqlCommand Get(Module.SearchModel model, SqlConnection connection) =>
            connection.CreateProcedureCommand(PROCEDURE)
            .AddPaginationParameters(model)
            .AddInParameter(PARAM_FILTERTEXT, model.FilterText)
            .AddInParameter(PARAM_FILTERBYAPPLICATION, model.FilterByApplication)
            .AddIntListParameter(PARAM_APPLICATIONIDS, model.ApplicationIds)
            .AddIntListParameter(PARAM_SKIPPEDIDS, model.SkippedIds);

        public async Task ExecuteAsync(Module.SearchModel model, Module.SearchResult result, SqlConnection connection, CancellationToken cancellationToken = default)
        {
            using var command = Get(model, connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (reader.HasRows)
                while (await reader.ReadAsync(cancellationToken))
                    result.Modules.Add(_readerConverter.Convert(reader));

            result.TotalCount = await ReadTotalCountAsync(reader, cancellationToken);
        }
    }
}
