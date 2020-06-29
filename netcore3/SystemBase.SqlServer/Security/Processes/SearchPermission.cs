using Sorschia.Data;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class SearchPermission : ProcessBase, ISearchPermission
    {
        private readonly SearchPermissionCommandProvider _commandProvider;

        public Permission.SearchModel Model { get; set; }

        public SearchPermission(IConnectionStringProvider connectionStringProvider, SearchPermissionCommandProvider commandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
        }

        public async Task<Permission.SearchResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var model = Model;
            var result = new Permission.SearchResult();
            using var connection = await OpenConnectionAsync(cancellationToken);
            await _commandProvider.ExecuteAsync(model, result, connection, cancellationToken);
            return result;
        }
    }
}
