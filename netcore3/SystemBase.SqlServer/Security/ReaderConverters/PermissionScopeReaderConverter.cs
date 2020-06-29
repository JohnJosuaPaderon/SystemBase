using Sorschia.Data;
using System.Data.SqlClient;
using SystemBase.Security.Entities;

namespace SystemBase.Security.ReaderConverters
{
    internal sealed class PermissionScopeReaderConverter
    {
        private const string FIELD_ID = "[Id]";
        private const string FIELD_NAME = "[Name]";

        public PermissionScope Convert(SqlDataReader reader) =>
            new PermissionScope
            {
                Id = reader.GetInt32(FIELD_ID),
                Name = reader.GetString(FIELD_NAME)
            };
    }
}
