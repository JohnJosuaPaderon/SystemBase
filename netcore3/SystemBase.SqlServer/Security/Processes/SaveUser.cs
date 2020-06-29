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
    internal sealed class SaveUser : ProcessBase, ISaveUser
    {
        private readonly SaveUserCommandProvider _commandProvider;
        private readonly SaveUserApplicationCommandProvider _saveUserApplicationCommandProvider;
        private readonly SaveUserModuleCommandProvider _saveUserModuleCommandProvider;
        private readonly SaveUserPermissionCommandProvider _saveUserPermissionCommandProvider;
        private readonly DeleteUserApplicationCommandProvider _deleteUserApplicationCommandProvider;
        private readonly DeleteUserModuleCommandProvider _deleteUserModuleCommandProvider;
        private readonly DeleteUserPermissionCommandProvider _deleteUserPermissionCommandProvider;

        public User.SaveModel Model { get; set; }

        public SaveUser(IConnectionStringProvider connectionStringProvider,
            SaveUserCommandProvider commandProvider,
            SaveUserApplicationCommandProvider saveUserApplicationCommandProvider,
            SaveUserModuleCommandProvider saveUserModuleCommandProvider,
            SaveUserPermissionCommandProvider saveUserPermissionCommandProvider,
            DeleteUserApplicationCommandProvider deleteUserApplicationCommandProvider,
            DeleteUserModuleCommandProvider deleteUserModuleCommandProvider,
            DeleteUserPermissionCommandProvider deleteUserPermissionCommandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
            _saveUserApplicationCommandProvider = saveUserApplicationCommandProvider;
            _saveUserModuleCommandProvider = saveUserModuleCommandProvider;
            _saveUserPermissionCommandProvider = saveUserPermissionCommandProvider;
            _deleteUserApplicationCommandProvider = deleteUserApplicationCommandProvider;
            _deleteUserModuleCommandProvider = deleteUserModuleCommandProvider;
            _deleteUserPermissionCommandProvider = deleteUserPermissionCommandProvider;
        }

        public async Task<User.SaveResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var user = Model.User;
                var userApplications = Model.UserApplications;
                var userModules = Model.UserModules;
                var userPermissions = Model.UserPermissions;
                var deletedUserApplicationIds = Model.DeletedUserApplicationIds;
                var deletedUserModuleIds = Model.DeletedUserModuleIds;
                var deletedUserPermissionIds = Model.DeletedUserPermissionIds;
                var result = new User.SaveResult();
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                await SaveAsync(user, result, connection, transaction, cancellationToken);
                await SaveUserApplicationsAsync(userApplications, result, connection, transaction, cancellationToken);
                await SaveUserModulesAsync(userModules, result, connection, transaction, cancellationToken);
                await SaveUserPermissionsAsync(userPermissions, result, connection, transaction, cancellationToken);
                await DeleteUserApplicationsAsync(deletedUserApplicationIds, result, connection, transaction, cancellationToken);
                await DeleteUserModulesAsync(deletedUserModuleIds, result, connection, transaction, cancellationToken);
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

        private async Task SaveAsync(User user, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _user = await _commandProvider.ExecuteAsync(user, connection, transaction, cancellationToken);

            if (_user is null)
                throw SystemBaseException.VariableIsNull<User>(nameof(_user));

            result.User = user;
        }

        private async Task SaveUserApplicationAsync(UserApplication userApplication, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _userApplication = await _saveUserApplicationCommandProvider.ExecuteAsync(userApplication, connection, transaction, cancellationToken);

            if (_userApplication is null)
                throw SystemBaseException.VariableIsNull<UserApplication>(nameof(_userApplication));

            result.UserApplications.Add(_userApplication);
        }

        private async Task SaveUserApplicationsAsync(IList<UserApplication> userApplications, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (userApplications != null && userApplications.Any())
                foreach (var userApplication in userApplications)
                {
                    userApplication.UserId = result.User.Id;
                    await SaveUserApplicationAsync(userApplication, result, connection, transaction, cancellationToken);
                }
        }

        private async Task SaveUserModuleAsync(UserModule userModule, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _userModule = await _saveUserModuleCommandProvider.ExecuteAsync(userModule, connection, transaction, cancellationToken);

            if (_userModule is null)
                throw SystemBaseException.VariableIsNull<UserModule>(nameof(_userModule));

            result.UserModules.Add(_userModule);
        }

        private async Task SaveUserModulesAsync(IList<UserModule> userModules, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (userModules != null && userModules.Any())
                foreach (var userModule in userModules)
                {
                    userModule.UserId = result.User.Id;
                    await SaveUserModuleAsync(userModule, result, connection, transaction, cancellationToken);
                }
        }

        private async Task SaveUserPermissionAsync(UserPermission userPermission, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _userPermission = await _saveUserPermissionCommandProvider.ExecuteAsync(userPermission, connection, transaction, cancellationToken);

            if (_userPermission is null)
                throw SystemBaseException.VariableIsNull<UserPermission>(nameof(_userPermission));

            result.UserPermissions.Add(_userPermission);
        }

        private async Task SaveUserPermissionsAsync(IList<UserPermission> userPermissions, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (userPermissions != null && userPermissions.Any())
                foreach (var userPermission in userPermissions)
                {
                    userPermission.UserId = result.User.Id;
                    await SaveUserPermissionAsync(userPermission, result, connection, transaction, cancellationToken);
                }
        }

        private async Task DeleteUserApplicationAsync(long id, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (await _deleteUserApplicationCommandProvider.ExecuteAsync(id, connection, transaction, cancellationToken))
                result.DeletedUserApplicationIds.Add(id);
        }

        private async Task DeleteUserApplicationsAsync(IList<long> ids, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (ids != null && ids.Any())
                foreach (var id in ids)
                    await DeleteUserApplicationAsync(id, result, connection, transaction, cancellationToken);
        }

        private async Task DeleteUserModuleAsync(long id, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (await _deleteUserModuleCommandProvider.ExecuteAsync(id, connection, transaction, cancellationToken))
                result.DeletedUserModuleIds.Add(id);
        }

        private async Task DeleteUserModulesAsync(IList<long> ids, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (ids != null && ids.Any())
                foreach (var id in ids)
                    await DeleteUserModuleAsync(id, result, connection, transaction, cancellationToken);
        }

        private async Task DeleteUserPermissionAsync(long id, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (await _deleteUserPermissionCommandProvider.ExecuteAsync(id, connection, transaction, cancellationToken))
                result.DeletedUserPermissionIds.Add(id);
        }

        private async Task DeleteUserPermissionsAsync(IList<long> ids, User.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (ids != null && ids.Any())
                foreach (var id in ids)
                    await DeleteUserPermissionAsync(id, result, connection, transaction, cancellationToken);
        }
    }
}
