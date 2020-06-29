using Sorschia.Data;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class SaveModulePermission : SavePermissionBase, ISaveModulePermission
    {
        private readonly SaveModulePermissionCommandProvider _commandProvider;

        public ModulePermission.SaveModel Model { get; set; }

        public SaveModulePermission(IConnectionStringProvider connectionStringProvider,
            SaveUserPermissionCommandProvider saveUserPermissionCommandProvider,
            DeleteUserPermissionCommandProvider deleteUserPermissionCommandProvider,
            SaveModulePermissionCommandProvider commandProvider) : base(connectionStringProvider, saveUserPermissionCommandProvider, deleteUserPermissionCommandProvider)
        {
            _commandProvider = commandProvider;
        }

        public async Task<ModulePermission.SaveResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var permission = Model.Permission;
                var userPermissions = Model.UserPermissions;
                var deletedUserPermissionIds = Model.DeletedUserPermissionIds;
                var result = new ModulePermission.SaveResult();
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                await SaveAsync(permission, result, connection, transaction, cancellationToken);
                await SaveUserPermissionsAsync(userPermissions, result.Permission.Id, result, connection, transaction, cancellationToken);
                await DeleteUserPermissionsAsync(deletedUserPermissionIds, result, connection, transaction, cancellationToken);
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                ThrowError(ex);
                return default;
            }
        }

        private async Task SaveAsync(ModulePermission permission, ModulePermission.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _permission = await _commandProvider.ExecuteAsync(permission, connection, transaction, cancellationToken);

            if (_permission is null)
                throw SystemBaseException.VariableIsNull<ModulePermission>(nameof(_permission));

            result.Permission = _permission;
        }
    }
}
