using Sorschia.Data;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class GetPlatform : ProcessBase, IGetPlatform
    {
        private readonly GetPlatformCommandProvider _commandProvider;

        public int Id { get; set; }

        public GetPlatform(IConnectionStringProvider connectionStringProvider, GetPlatformCommandProvider commandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
        }

        public async Task<Platform> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var id = Id;
            using var connection = await OpenConnectionAsync(cancellationToken);
            return await _commandProvider.ExecuteAsync(id, connection, cancellationToken);
        }
    }
}
