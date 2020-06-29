using Sorschia.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class GetUser : ProcessBase, IGetUser
    {
        private readonly GetUserCommandProvider _commandProvider;

        public int Id { get; set; }

        public GetUser(IConnectionStringProvider connectionStringProvider, GetUserCommandProvider commandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
        }

        public async Task<User> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var id = Id;
            using var connection = await OpenConnectionAsync(cancellationToken);
            return await _commandProvider.ExecuteAsync(id, connection, cancellationToken);
        }
    }
}
