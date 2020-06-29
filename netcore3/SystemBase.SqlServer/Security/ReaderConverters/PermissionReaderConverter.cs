using Sorschia.Data;
using System.Data.SqlClient;
using SystemBase.Security.Entities;

namespace SystemBase.Security.ReaderConverters
{
    internal sealed class PermissionReaderConverter
    {
        private const string FIELD_ID = "[Id]";
        private const string FIELD_NAME = "[Name]";
        private const string FIELD_CODE = "[Code]";
        private const string FIELD_SCOPEID = "[ScopeId]";

        public Permission Convert(SqlDataReader reader) =>
            new Permission
            {
                Id = reader.GetInt32(FIELD_ID),
                Name = reader.GetString(FIELD_NAME),
                Code = reader.GetString(FIELD_CODE),
                ScopeId = reader.GetNullableInt32(FIELD_SCOPEID)
            };
    }
}
