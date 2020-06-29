using Sorschia.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal abstract class SavePermissionBase : ProcessBase
    {
        private readonly SaveUserPermissionCommandProvider _saveUserPermissionCommandProvider;
        private readonly DeleteUserPermissionCommandProvider _deleteUserPermissionCommandProvider;

        protected SavePermissionBase(IConnectionStringProvider connectionStringProvider,
            SaveUserPermissionCommandProvider saveUserPermissionCommandProvider,
            DeleteUserPermissionCommandProvider deleteUserPermissionCommandProvider) : base(connectionStringProvider)
        {
            _saveUserPermissionCommandProvider = saveUserPermissionCommandProvider;
            _deleteUserPermissionCommandProvider = deleteUserPermissionCommandProvider;
        }

        private async Task SaveUserPermissionAsync(UserPermission userPermission, Permission.SaveResultBase result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _userPermission = await _saveUserPermissionCommandProvider.ExecuteAsync(userPermission, connection, transaction, cancellationToken);

            if (_userPermission is null)
                throw SystemBaseException.VariableIsNull<UserPermission>(nameof(_userPermission));

            result.UserPermissions.Add(_userPermission);
        }

        protected async Task SaveUserPermissionsAsync(IList<UserPermission> userPermissions, int permissionId, Permission.SaveResultBase result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (userPermissions != null && userPermissions.Any())
                foreach (var userPermission in userPermissions)
                {
                    userPermission.PermissionId = permissionId;
                    await SaveUserPermissionAsync(userPermission, result, connection, transaction, cancellationToken);
                }
        }

        private async Task DeleteUserPermissionAsync(long id, Permission.SaveResultBase result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (await _deleteUserPermissionCommandProvider.ExecuteAsync(id, connection, transaction, cancellationToken))
                result.DeletedUserPermissionIds.Add(id);
        }

        protected async Task DeleteUserPermissionsAsync(IList<long> ids, Permission.SaveResultBase result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (ids != null && ids.Any())
                foreach (var id in ids)
                    await DeleteUserPermissionAsync(id, result, connection, transaction, cancellationToken);
        }
    }
}
