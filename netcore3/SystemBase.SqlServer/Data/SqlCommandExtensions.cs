using Sorschia.Data;
using System.Data.SqlClient;
using SystemBase.Security;

namespace SystemBase.Data
{
    internal static class SqlCommandExtensions
    {
        public static SqlCommand AddSessionIdParameter(this SqlCommand instance, ICurrentSessionProvider currentSessionProvider, string parameterName = default)
        {
            return instance.AddInParameter(string.IsNullOrWhiteSpace(parameterName) ? "@SessionId" : parameterName, currentSessionProvider.GetId());
        }
    }
}
