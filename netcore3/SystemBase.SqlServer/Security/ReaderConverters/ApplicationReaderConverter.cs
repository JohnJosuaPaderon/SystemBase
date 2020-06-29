using Sorschia.Data;
using System.Data.SqlClient;
using SystemBase.Security.Entities;

namespace SystemBase.Security.ReaderConverters
{
    internal sealed class ApplicationReaderConverter
    {
        private const string FIELD_ID = "[Id]";
        private const string FIELD_NAME = "[Name]";
        private const string FIELD_PLATFORMID = "[PlatformId]";

        public Application Convert(SqlDataReader reader) =>
            new Application
            {
                Id = reader.GetInt32(FIELD_ID),
                Name = reader.GetString(FIELD_NAME),
                PlatformId = reader.GetNullableInt32(FIELD_PLATFORMID)
            };
    }
}
