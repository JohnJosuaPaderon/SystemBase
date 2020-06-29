using System.Threading;
using System.Threading.Tasks;
using SystemBase.Security.Repositories;

namespace SystemBase.Security.Entities
{
    public static class ModulePermissionExtensions
    {
        public static async Task<Module> GetModuleAsync(this ModulePermission instance, IModuleRepository repository, CancellationToken cancellationToken = default)
        {
            if (instance is null)
                return null;

            if (repository is null)
                throw SystemBaseException.RepositoryIsNull<IModuleRepository>();

            var moduleId = instance.ModuleId;

            if (moduleId > 0)
            {
                var module = await repository.GetAsync(moduleId ?? 0, cancellationToken);
                instance.Module = module;
                return module;
            }

            return null;
        }
    }
}
