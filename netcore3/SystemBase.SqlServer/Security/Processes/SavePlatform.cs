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
    internal sealed class SavePlatform : ProcessBase, ISavePlatform
    {
        private readonly SavePlatformCommandProvider _commandProvider;
        private readonly SaveApplicationCommandProvider _saveApplicationCommandProvider;
        private readonly DeleteApplicationCommandProvider _deleteApplicationCommandProvider;

        public Platform.SaveModel Model { get; set; }

        public SavePlatform(IConnectionStringProvider connectionStringProvider,
            SavePlatformCommandProvider commandProvider,
            SaveApplicationCommandProvider saveApplicationCommandProvider,
            DeleteApplicationCommandProvider deleteApplicationCommandProvider) : base(connectionStringProvider)
        {
            _commandProvider = commandProvider;
            _saveApplicationCommandProvider = saveApplicationCommandProvider;
            _deleteApplicationCommandProvider = deleteApplicationCommandProvider;
        }

        public async Task<Platform.SaveResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var platform = Model.Platform;
                var applications = Model.Applications;
                var deletedApplications = Model.DeletedApplications;
                var result = new Platform.SaveResult();
                using var connection = await OpenConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();
                await SaveAsync(platform, result, connection, transaction, cancellationToken);
                await SaveApplicationsAsync(applications, result, connection, transaction, cancellationToken);
                await DeleteApplicationsAsync(deletedApplications, result, connection, transaction, cancellationToken);
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                ThrowError(ex);
                return default;
            }
        }

        private async Task SaveAsync(Platform platform, Platform.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _platform = await _commandProvider.ExecuteAsync(platform, connection, transaction, cancellationToken);

            if (_platform is null)
                throw SystemBaseException.VariableIsNull<Platform>(nameof(_platform));

            result.Platform = _platform;
        }

        private async Task SaveApplicationAsync(Application application, Platform.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var _permission = await _saveApplicationCommandProvider.ExecuteAsync(application, connection, transaction, cancellationToken);

            if (_permission is null)
                throw SystemBaseException.VariableIsNull<Application>(nameof(_permission));

            result.Applications.Add(_permission);
        }

        private async Task SaveApplicationsAsync(IList<Application> applications, Platform.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (applications != null && applications.Any())
                foreach (var appliation in applications)
                {
                    appliation.PlatformId = result.Platform.Id;
                    await SaveApplicationAsync(appliation, result, connection, transaction, cancellationToken);
                }
        }

        private async Task DeleteApplicationAsync(Application.DeleteModel model, Platform.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (await _deleteApplicationCommandProvider.ExecuteAsync(model, connection, transaction, cancellationToken))
                result.DeletedApplicationIds.Add(model.Id);
        }

        private async Task DeleteApplicationsAsync(IList<Application.DeleteModel> models, Platform.SaveResult result, SqlConnection connection, SqlTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            if (models != null && models.Any())
                foreach (var model in models)
                    await DeleteApplicationAsync(model, result, connection, transaction, cancellationToken);
        }
    }
}
