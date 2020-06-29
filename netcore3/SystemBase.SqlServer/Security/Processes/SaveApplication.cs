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
    internal sealed class SaveApplication : ProcessBase, ISaveApplication
    {
        private readonly SaveApplicationCommandProvider _commandProvider;
        private readonly SaveApplicationPermissionCommandProvider _savePermissionCommandProvider;
        private readonly SaveModuleCommandProvider _saveModuleCommandProvider;
        private readonly SaveUserApplicationCommandProvider _saveUserApplicationCommandProvider;
        private readonly DeletePermissionCommandProvider _deletePermissionCommandProvider;
        private readonly DeleteModuleCommandProvider _deleteModuleCommandProvider;
        private readonly DeleteUserApplicationCommandProvider _deleteUserApplicationCommandProvider;

        public Application.SaveModel Model { get; set; }

        public SaveApplication(IConnectionStringProvider connectionStringProvider,
            SaveApplicationCommandProvider commandProvider,
            SaveApplicationPermissionCommandProvider savePermissionCommandProvider,
            SaveModuleCommandProvider saveModuleCommandProvider,
            SaveUserApplicationCommandProvider saveUserApplicationCommandProvider,
            DeletePermissionCommandProvider deletePermissionCommandProvider,
            DeleteModuleCommandProvider deleteModuleCommandProvider,
            DeleteUserApplicationCommandProvider deleteUserApplicationCommandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
            _savePermissionCommandProvider = savePermissionCommandProvider;
            _saveModuleCommandProvider = saveModuleCommandProvider;
            _saveUserApplicationCommandProvider = saveUserApplicationCommandProvider;
            _deletePermissionCommandProvider = deletePermissionCommandProvider;
            _deleteModuleCommandProvider = deleteModuleCommandProvider;
            _deleteUserApplicationCommandProvider = deleteUserApplicationCommandProvider;
        }

        public async Task<Application.SaveResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var application = Model.Application;
                var permissions = Model.Permissions;
                var modules = Model.Modules;
                var userApplications = Model.UserApplications;
                var deletedPermissions = Model.DeletedPermissions;
                var deletedModules = Model.DeletedModules;
                var deletedUserApplicationIds = Model.DeletedUserApplicationIds;
                var result = new Application.SaveResult();
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                await SaveAsync(application, result, connection, transaction, cancellationToken);
                await SavePermissionsAsync(permissions, result, connection, transaction, cancellationToken);
                await SaveModulesAsync(modules, result, connection, transaction, cancellationToken);
                await SaveUserApplicationsAsync(userApplications, result, connection, transaction, cancellationToken);
                await DeletePermissionsAsync(deletedPermissions, result, connection, transaction, cancellationToken);
                await DeleteModulesAsync(deletedModules, result, connection, transaction, cancellationToken);
                await DeleteUserApplicationsAsync(deletedUserApplicationIds, result, connection, transaction, cancellationToken);
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                ThrowError(ex);
                return default;
            }
        }

        private async Task SaveAsync(Application application, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _application = await _commandProvider.ExecuteAsync(application, connection, transaction, cancellationToken);

            if (_application is null)
                throw SystemBaseException.VariableIsNull<Application>(nameof(_application));

            result.Application = _application;
        }

        private async Task SavePermissionAsync(ApplicationPermission permission, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _permission = await _savePermissionCommandProvider.ExecuteAsync(permission, connection, transaction, cancellationToken);

            if (_permission is null)
                throw SystemBaseException.VariableIsNull<ApplicationPermission>(nameof(_permission));

            result.Permissions.Add(_permission);
        }

        private async Task SavePermissionsAsync(IList<ApplicationPermission> permissions, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (permissions != null && permissions.Any())
                foreach (var permission in permissions)
                {
                    permission.ApplicationId = result.Application.Id;
                    await SavePermissionAsync(permission, result, connection, transaction, cancellationToken);
                }
        }

        private async Task SaveModuleAsync(Module module, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _module = await _saveModuleCommandProvider.ExecuteAsync(module, connection, transaction, cancellationToken);

            if (_module is null)
                throw SystemBaseException.VariableIsNull<Module>(nameof(_module));

            result.Modules.Add(_module);
        }

        private async Task SaveModulesAsync(IList<Module> modules, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (modules != null && modules.Any())
                foreach (var module in modules)
                {
                    module.ApplicationId = result.Application.Id;
                    await SaveModuleAsync(module, result, connection, transaction, cancellationToken);
                }
        }

        private async Task SaveUserApplicationAsync(UserApplication userApplication, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _userApplication = await _saveUserApplicationCommandProvider.ExecuteAsync(userApplication, connection, transaction, cancellationToken);

            if (_userApplication is null)
                throw SystemBaseException.VariableIsNull<UserApplication>(nameof(_userApplication));

            result.UserApplications.Add(_userApplication);
        }

        private async Task SaveUserApplicationsAsync(IList<UserApplication> userApplications, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (userApplications != null && userApplications.Any())
                foreach (var userApplication in userApplications)
                {
                    userApplication.ApplicationId = result.Application.Id;
                    await SaveUserApplicationAsync(userApplication, result, connection, transaction, cancellationToken);
                }
        }

        private async Task DeletePermissionAsync(Permission.DeleteModel model, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (await _deletePermissionCommandProvider.ExecuteAsync(model, connection, transaction, cancellationToken))
                result.DeletedPermissionIds.Add(model.Id);
        }

        private async Task DeletePermissionsAsync(IList<Permission.DeleteModel> models, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (models != null && models.Any())
                foreach (var model in models)
                    await DeletePermissionAsync(model, result, connection, transaction, cancellationToken);
        }

        private async Task DeleteModuleAsync(Module.DeleteModel model, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (await _deleteModuleCommandProvider.ExecuteAsync(model, connection, transaction, cancellationToken))
                result.DeletedModuleIds.Add(model.Id);
        }

        private async Task DeleteModulesAsync(IList<Module.DeleteModel> models, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (models != null && models.Any())
                foreach (var model in models)
                    await DeleteModuleAsync(model, result, connection, transaction, cancellationToken);
        }

        private async Task DeleteUserApplicationAsync(long id, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (await _deleteUserApplicationCommandProvider.ExecuteAsync(id, connection, transaction, cancellationToken))
                result.DeletedUserApplicationIds.Add(id);
        }

        private async Task DeleteUserApplicationsAsync(IList<long> ids, Application.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (ids != null && ids.Any())
                foreach (var id in ids)
                    await DeleteUserApplicationAsync(id, result, connection, transaction, cancellationToken);
        }
    }
}
