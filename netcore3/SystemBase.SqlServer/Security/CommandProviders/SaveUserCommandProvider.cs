using Sorschia.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Data;
using SystemBase.Security.Entities;

namespace SystemBase.Security.CommandProviders
{
    internal sealed class SaveUserCommandProvider
    {
        private const string PROCEDURE = "[Security].[SaveUser]";
        private const string PARAM_ID = "@Id";
        private const string PARAM_FIRSTNAME = "@FirstName";
        private const string PARAM_MIDDLENAME = "@MiddleName";
        private const string PARAM_LASTNAME = "@LastName";
        private const string PARAM_USERNAME = "@Username";
        private const string PARAM_PASSWORDHASH = "@PasswordHash";
        private const string PARAM_ISACTIVE = "@IsActive";
        private const string PARAM_ISPASSWORDCHANGEREQUIRED = "@IsPasswordChangeRequired";
        private const SqlDbType PARAMTYPE_ID = SqlDbType.Int;
        private const int ALLOWEDAFFECTEDROWS = 1;

        private readonly ICurrentSessionProvider _sessionProvider;
        private readonly ISystemUserPasswordCryptoHash _passwordCryptoHash;

        public SaveUserCommandProvider(ICurrentSessionProvider sessionProvider, ISystemUserPasswordCryptoHash passwordCryptoHash)
        {
            _sessionProvider = sessionProvider;
            _passwordCryptoHash = passwordCryptoHash;
        }

        private SqlCommand Get(User user, SqlConnection connection, SqlTransaction transaction = default) =>
            connection.CreateProcedureCommand(PROCEDURE, transaction)
            .AddInOutParameter(PARAM_ID, user.Id, PARAMTYPE_ID)
            .AddInParameter(PARAM_FIRSTNAME, user.FirstName)
            .AddInParameter(PARAM_MIDDLENAME, user.MiddleName)
            .AddInParameter(PARAM_LASTNAME, user.LastName)
            .AddInParameter(PARAM_USERNAME, user.Username)
            .AddInParameter(PARAM_PASSWORDHASH, _passwordCryptoHash.ComputeHash(user.Password))
            .AddInParameter(PARAM_ISACTIVE, user.IsActive)
            .AddInParameter(PARAM_ISPASSWORDCHANGEREQUIRED, user.IsPasswordChangeRequired)
            .AddSessionIdParameter(_sessionProvider);

        public async Task<User> ExecuteAsync(User user, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            using var command = Get(user, connection, transaction);

            if (await command.ExecuteNonQueryAsync(cancellationToken) == ALLOWEDAFFECTEDROWS)
            {
                user.Id = command.Parameters.GetInt32(PARAM_ID);
                return user;
            }

            return default;
        }
    }
}
