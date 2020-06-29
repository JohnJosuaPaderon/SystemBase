using Sorschia.Data;
using System.Data.SqlClient;
using SystemBase.Security.Entities;

namespace SystemBase.Security.ReaderConverters
{
    internal sealed class UserReaderConverter
    {
        private const string FIELD_ID = "[Id]";
        private const string FIELD_FIRSTNAME = "[FirstName]";
        private const string FIELD_MIDDLENAME = "[MiddleName]";
        private const string FIELD_LASTNAME = "[LastName]";
        private const string FIELD_USERNAME = "[Username]";
        private const string FIELD_ISACTIVE = "[IsActive]";
        private const string FIELD_ISPASSWORDCHANGEREQUIRED = "[IsPasswordChangeRequired]";

        public User Convert(SqlDataReader reader) =>
            new User
            {
                Id = reader.GetInt32(FIELD_ID),
                FirstName = reader.GetString(FIELD_FIRSTNAME),
                MiddleName = reader.GetString(FIELD_MIDDLENAME),
                LastName = reader.GetString(FIELD_LASTNAME),
                Username = reader.GetString(FIELD_USERNAME),
                IsActive = reader.GetBoolean(FIELD_ISACTIVE),
                IsPasswordChangeRequired = reader.GetBoolean(FIELD_ISPASSWORDCHANGEREQUIRED)
            };
    }
}
