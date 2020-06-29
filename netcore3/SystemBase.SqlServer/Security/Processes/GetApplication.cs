using Sorschia.Data;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class GetApplication : ProcessBase, IGetApplication
    {
        private readonly GetApplicationCommandProvider _commandProvider;

        public int Id { get; set; }

        public GetApplication(IConnectionStringProvider connectionStringProvider, GetApplicationCommandProvider commandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
        }
        
        public async Task<Application> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var id = Id;
            using var connection = await OpenConnectionAsync(cancellationToken);
            return await _commandProvider.ExecuteAsync(id, connection, cancellationToken);
        }
    }
}
