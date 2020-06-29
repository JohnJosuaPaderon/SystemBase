using Sorschia.Data;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class SearchPermissionScope : ProcessBase, ISearchPermissionScope
    {
        private readonly SearchPermissionScopeCommandProvider _commandProvider;

        public PermissionScope.SearchModel Model { get; set; }

        public SearchPermissionScope(IConnectionStringProvider connectionStringProvider, SearchPermissionScopeCommandProvider commandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
        }

        public async Task<PermissionScope.SearchResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var model = Model;
            var result = new PermissionScope.SearchResult();
            using var connection = await OpenConnectionAsync(cancellationToken);
            await _commandProvider.ExecuteAsync(model, result, connection, cancellationToken);
            return result;
        }
    }
}
