using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Repositories;

namespace SystemBase.Security.Entities
{
    public static class ModuleExtensions
    {
        public static async Task<Application> GetApplicationAsync(this Module instance, IApplicationRepository repository, CancellationToken cancellationToken = default)
        {
            if (instance is null)
                return null;

            if (repository is null)
                throw SystemBaseException.RepositoryIsNull< IApplicationRepository>();

            var applicationId = instance.ApplicationId;

            if (applicationId > 0)
            {
                var application = await repository.GetAsync(applicationId ?? 0, cancellationToken);
                instance.Application = application;
                return application;
            }

            return null;
        }
    }
}
