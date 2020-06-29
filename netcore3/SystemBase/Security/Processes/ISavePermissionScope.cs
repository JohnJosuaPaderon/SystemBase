using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISavePermissionScope : IAsyncProcess<PermissionScope.SaveResult>
    {
        PermissionScope.SaveModel Model { get; set; }
    }
}
