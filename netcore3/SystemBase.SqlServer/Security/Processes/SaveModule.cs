using Sorschia.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.CommandProviders;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    internal sealed class SaveModule : ProcessBase, ISaveModule
    {
        private readonly SaveModuleCommandProvider _commandProvider;
        private readonly SaveModulePermissionCommandProvider _saveModulePermissionCommandProvider;
        private readonly SaveUserModuleCommandProvider _saveUserModuleCommandProvider;
        private readonly DeletePermissionCommandProvider _deletePermissionCommandProvider;
        private readonly DeleteUserModuleCommandProvider _deleteUserModuleCommandProvider;

        public Module.SaveModel Model { get; set; }

        public SaveModule(IConnectionStringProvider connectionStringProvider,
            SaveModuleCommandProvider commandProvider,
            SaveModulePermissionCommandProvider saveModulePermissionCommandProvider,
            SaveUserModuleCommandProvider saveUserModuleCommandProvider,
            DeletePermissionCommandProvider deletePermissionCommandProvider,
            DeleteUserModuleCommandProvider deleteUserModuleCommandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
            _saveModulePermissionCommandProvider = saveModulePermissionCommandProvider;
            _saveUserModuleCommandProvider = saveUserModuleCommandProvider;
            _deletePermissionCommandProvider = deletePermissionCommandProvider;
            _deleteUserModuleCommandProvider = deleteUserModuleCommandProvider;
        }

        public async Task<Module.SaveResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var module = Model.Module;
                var permissions = Model.Permissions;
                var userModules = Model.UserModules;
                var deletedPermissions = Model.DeletedPermissions;
                var deletedUserModuleIds = Model.DeletedUserModuleIds;
                var result = new Module.SaveResult();
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                await SaveAsync(module, result, connection, transaction, cancellationToken);
                await SavePermissionsAsync(permissions, result, connection, transaction, cancellationToken);
                await SaveUserModulesAsync(userModules, result, connection, transaction, cancellationToken);
                await DeletePermissionsAsync(deletedPermissions, result, connection, transaction, cancellationToken);
                await DeleteUserModulesAsync(deletedUserModuleIds, result, connection, transaction, cancellationToken);
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                ThrowError(ex);
                return default;
            }
        }

        private async Task SaveAsync(Module module, Module.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _module = await _commandProvider.ExecuteAsync(module, connection, transaction, cancellationToken);

            if (_module is null)
                throw SystemBaseException.VariableIsNull<Module>(nameof(_module));

            result.Module = _module;
        }

        private async Task SavePermissionAsync(ModulePermission permission, Module.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _permission = await _saveModulePermissionCommandProvider.ExecuteAsync(permission, connection, transaction, cancellationToken);

            if (_permission is null)
                throw SystemBaseException.VariableIsNull<ModulePermission>(nameof(_permission));

            result.Permissions.Add(_permission);
        }

        private async Task SavePermissionsAsync(IList<ModulePermission> permissions, Module.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (permissions != null && permissions.Any())
                foreach (var permission in permissions)
                {
                    permission.ModuleId = result.Module.Id;
                    await SavePermissionAsync(permission, result, connection, transaction, cancellationToken);
                }
        }

        private async Task SaveUserModuleAsync(UserModule userModule, Module.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _userModule = await _saveUserModuleCommandProvider.ExecuteAsync(userModule, connection, transaction, cancellationToken);

            if (_userModule is null)
                throw SystemBaseException.VariableIsNull<UserModule>(nameof(_userModule));

            result.UserModules.Add(_userModule);
        }

        private async Task SaveUserModulesAsync(IList<UserModule> userModules, Module.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (userModules != null && userModules.Any())
                foreach (var userModule in userModules)
                {
                    userModule.ModuleId = result.Module.Id;
                    await SaveUserModuleAsync(userModule, result, connection, transaction, cancellationToken);
                }
        }

        private async Task DeletePermissionAsync(Permission.DeleteModel model, Module.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (await _deletePermissionCommandProvider.ExecuteAsync(model, connection, transaction, cancellationToken))
                result.DeletedPermissionIds.Add(model.Id);
        }

        private async Task DeletePermissionsAsync(IList<Permission.DeleteModel> models, Module.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (models != null && models.Any())
                foreach (var model in models)
                    await DeletePermissionAsync(model, result, connection, transaction, cancellationToken);
        }

        private async Task DeleteUserModuleAsync(long id, Module.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (await _deleteUserModuleCommandProvider.ExecuteAsync(id, connection, transaction, cancellationToken))
                result.DeletedUserModuleIds.Add(id);
        }

        private async Task DeleteUserModulesAsync(IList<long> ids, Module.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (ids != null && ids.Any())
                foreach (var id in ids)
                    await DeleteUserModuleAsync(id, result, connection, transaction, cancellationToken);
        }
    }
}
