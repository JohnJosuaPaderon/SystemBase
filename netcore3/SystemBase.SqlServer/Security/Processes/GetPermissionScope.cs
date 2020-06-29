using Sorschia.Data;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class GetPermissionScope : ProcessBase, IGetPermissionScope
    {
        private readonly GetPermissionScopeCommandProvider _commandProvider;

        public int Id { get; set; }

        public GetPermissionScope(IConnectionStringProvider connectionStringProvider, GetPermissionScopeCommandProvider commandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
        }

        public async Task<PermissionScope> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var id = Id;
            using var connection = await OpenConnectionAsync(cancellationToken);
            return await _commandProvider.ExecuteAsync(id, connection, cancellationToken);
        }
    }
}
