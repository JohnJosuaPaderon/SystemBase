using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IDeletePermissionScope : IAsyncProcess<bool>
    {
        PermissionScope.DeleteModel Model { get; set; }
    }
}
