using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace SystemBase.CommandProviders
{
    public abstract class PaginationCommandProviderBase
    {
        protected async Task<int> ReadTotalCountAsync(SqlDataReader reader, CancellationToken cancellationToken = default) =>
            await reader.NextResultAsync(cancellationToken) && await reader.ReadAsync(cancellationToken) ? reader.GetInt32(0) : default;
    }
}
