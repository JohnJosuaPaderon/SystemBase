using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISaveModulePermission : IAsyncProcess<ModulePermission.SaveResult>
    {
        ModulePermission.SaveModel Model { get; set; }
    }
}
