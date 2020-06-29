using Sorschia.Data;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class SearchModule : ProcessBase, ISearchModule
    {
        private readonly SearchModuleCommandProvider _commandProvider;

        public Module.SearchModel Model { get; set; }

        public SearchModule(IConnectionStringProvider connectionStringProvider, SearchModuleCommandProvider commandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
        }

        public async Task<Module.SearchResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var model = Model;
            var result = new Module.SearchResult();
            using var connection = await OpenConnectionAsync(cancellationToken);
            await _commandProvider.ExecuteAsync(model, result, connection, cancellationToken);
            return result;
        }
    }
}
