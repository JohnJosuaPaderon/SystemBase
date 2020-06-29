using Sorschia.Data;
using System.Data.SqlClient;
using SystemBase.Security.Entities;

namespace SystemBase.Security.ReaderConverters
{
    internal sealed class PlatformReaderConverter
    {
        private const string FIELD_ID = "[Id]";
        private const string FIELD_NAME = "[Name]";

        public Platform Convert(SqlDataReader reader) =>
            new Platform
            {
                Id = reader.GetInt32(FIELD_ID),
                Name = reader.GetString(FIELD_NAME)
            };
    }
}
