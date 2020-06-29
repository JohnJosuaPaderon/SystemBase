using Sorschia.Data;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class SearchApplication : ProcessBase, ISearchApplication
    {
        private readonly SearchApplicationCommandProvider _commandProvider;

        public Application.SearchModel Model { get; set; }

        public SearchApplication(IConnectionStringProvider connectionStringProvider, SearchApplicationCommandProvider commandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
        }

        public async Task<Application.SearchResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var model = Model;
            var result = new Application.SearchResult();
            using var connection = await OpenConnectionAsync(cancellationToken);
            await _commandProvider.ExecuteAsync(model, result, connection, cancellationToken);
            return result;
        }
    }
}
