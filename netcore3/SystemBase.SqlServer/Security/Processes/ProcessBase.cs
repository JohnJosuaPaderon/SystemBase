using Sorschia.Data;
using Sorschia.Processes;
using System.Data.SqlClient;

namespace SystemBase.Security.Processes
{
    internal abstract class ProcessBase : SqlProcessBase
    {
        private IConnectionStringProvider _connectionStringProvider;

        public ProcessBase(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        protected override SqlConnection InitializeConnection() => new SqlConnection(_connectionStringProvider["systemBase"]);
    }
}
