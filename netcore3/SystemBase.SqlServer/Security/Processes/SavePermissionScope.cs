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
    internal sealed class SavePermissionScope : ProcessBase, ISavePermissionScope
    {
        private readonly SavePermissionScopeCommandProvider _commandProvider;
        private readonly SavePermissionCommandProvider _savePermissionCommandProvider;
        private readonly DeletePermissionCommandProvider _deletePermissionCommandProvider;

        public PermissionScope.SaveModel Model { get; set; }

        public SavePermissionScope(IConnectionStringProvider connectionStringProvider,
            SavePermissionScopeCommandProvider commandProvider,
            SavePermissionCommandProvider savePermissionCommandProvider,
            DeletePermissionCommandProvider deletePermissionCommandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
            _savePermissionCommandProvider = savePermissionCommandProvider;
            _deletePermissionCommandProvider = deletePermissionCommandProvider;
        }

        public async Task<PermissionScope.SaveResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var scope = Model.Scope;
                var permissions = Model.Permissions;
                var deletedPermissions = Model.DeletedPermissions;
                var result = new PermissionScope.SaveResult();
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                await SaveAsync(scope, result, connection, transaction, cancellationToken);
                await SavePermissionsAsync(permissions, result, connection, transaction, cancellationToken);
                await DeletePermissionsAsync(deletedPermissions, result, connection, transaction, cancellationToken);
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                ThrowError(ex);
                return default;
            }
        }

        private async Task SaveAsync(PermissionScope scope, PermissionScope.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _scope = await _commandProvider.ExecuteAsync(scope, connection, transaction, cancellationToken);

            if (_scope is null)
                throw SystemBaseException.VariableIsNull<PermissionScope>(nameof(_scope));

            result.Scope = _scope;
        }

        private async Task SavePermissionAsync(Permission permission, PermissionScope.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _permission = await _savePermissionCommandProvider.ExecuteAsync(permission, connection, transaction, cancellationToken);

            if (_permission is null)
                throw SystemBaseException.VariableIsNull<Permission>(nameof(_permission));

            result.Permissions.Add(_permission);
        }

        private async Task SavePermissionsAsync(IList<Permission> permissions, PermissionScope.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (permissions != null && permissions.Any())
                foreach (var permission in permissions)
                {
                    permission.ScopeId = result.Scope.Id;
                    await SavePermissionAsync(permission, result, connection, transaction, cancellationToken);
                }
        }

        private async Task DeletePermissionAsync(Permission.DeleteModel model, PermissionScope.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (await _deletePermissionCommandProvider.ExecuteAsync(model, connection, transaction, cancellationToken))
                result.DeletedPermissionIds.Add(model.Id);
        }

        private async Task DeletePermissionsAsync(IList<Permission.DeleteModel> models, PermissionScope.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (models != null && models.Any())
                foreach (var model in models)
                    await DeletePermissionAsync(model, result, connection, transaction, cancellationToken);
        }
    }
}
