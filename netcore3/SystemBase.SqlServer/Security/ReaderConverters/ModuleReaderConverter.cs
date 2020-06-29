using Sorschia.Data;
using System.Data.SqlClient;
using SystemBase.Security.Entities;

namespace SystemBase.Security.ReaderConverters
{
    internal sealed class ModuleReaderConverter
    {
        private const string FIELD_ID = "[Id]";
        private const string FIELD_NAME = "[Name]";
        private const string FIELD_ORDINALNUMBER = "[OrdinalNumber]";
        private const string FIELD_APPLICATIONID = "[ApplicationId]";

        public Module Convert(SqlDataReader reader) =>
            new Module
            {
                Id = reader.GetInt32(FIELD_ID),
                Name = reader.GetString(FIELD_NAME),
                OrdinalNumber = reader.GetInt32(FIELD_ORDINALNUMBER),
                ApplicationId = reader.GetNullableInt32(FIELD_APPLICATIONID)
            };
    }
}
