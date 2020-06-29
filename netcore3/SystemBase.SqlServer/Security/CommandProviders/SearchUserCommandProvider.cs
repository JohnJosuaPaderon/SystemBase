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
    internal sealed class SearchUserCommandProvider : PaginationCommandProviderBase
    {
        private const string PROCEDURE = "[Security].[SearchUser]";
        private const string PARAM_FILTERTEXT = "@FilterText";
        private const string PARAM_ISACTIVE = "@IsActive";
        private const string PARAM_ISPASSWORDCHANGEREQUIRED = "@IsPasswordChangeRequired";
        private const string PARAM_SKIPPEDIDS = "@SkippedIds";

        private readonly UserReaderConverter _readerConverter;

        public SearchUserCommandProvider(UserReaderConverter readerConverter)
        {
            _readerConverter = readerConverter;
        }

        private SqlCommand Get(User.SearchModel model, SqlConnection connection) =>
            connection.CreateProcedureCommand(PROCEDURE)
            .AddPaginationParameters(model)
            .AddInParameter(PARAM_FILTERTEXT, model.FilterText)
            .AddInParameter(PARAM_ISACTIVE, model.IsActive)
            .AddInParameter(PARAM_ISPASSWORDCHANGEREQUIRED, model.IsPasswordChangeRequired)
            .AddIntListParameter(PARAM_SKIPPEDIDS, model.SkippedIds);

        public async Task ExecuteAsync(User.SearchModel model, User.SearchResult result, SqlConnection connection, CancellationToken cancellationToken = default)
        {
            using var command = Get(model, connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (reader.HasRows)
                while (await reader.ReadAsync(cancellationToken))
                    result.Users.Add(_readerConverter.Convert(reader));

            result.TotalCount = await ReadTotalCountAsync(reader, cancellationToken);
        }
    }
}
