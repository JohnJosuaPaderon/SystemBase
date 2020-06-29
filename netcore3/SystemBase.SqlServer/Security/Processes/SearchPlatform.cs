using Sorschia.Data;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class SearchPlatform : ProcessBase, ISearchPlatform
    {
        private readonly SearchPlatformCommandProvider _commandProvider;

        public Platform.SearchModel Model { get; set; }

        public SearchPlatform(IConnectionStringProvider connectionStringProvider, SearchPlatformCommandProvider commandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
        }

        public async Task<Platform.SearchResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var model = Model;
            var result = new Platform.SearchResult();
            using var connection = await OpenConnectionAsync(cancellationToken);
            await _commandProvider.ExecuteAsync(model, result, connection, cancellationToken);
            return result;
        }
    }
}
