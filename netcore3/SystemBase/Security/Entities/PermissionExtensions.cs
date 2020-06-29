using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Repositories;

namespace SystemBase.Security.Entities
{
    public static class PermissionExtensions
    {
        public static async Task<PermissionScope> GetScopeAsync(this Permission instance, IPermissionScopeRepository repository, CancellationToken cancellationToken = default)
        {
            if (instance is null)
                return null;

            if (repository is null)
                throw SystemBaseException.RepositoryIsNull<IPermissionScopeRepository>();

            var scopeId = instance.ScopeId;

            if (scopeId > 0)
            {
                var scope = await repository.GetAsync(scopeId ?? 0, cancellationToken);
                instance.Scope = scope;
                return scope;
            }

            return null;
        }
    }
}
