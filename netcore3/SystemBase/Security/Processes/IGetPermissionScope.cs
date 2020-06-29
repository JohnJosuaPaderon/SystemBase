using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IGetPermissionScope : IAsyncProcess<PermissionScope>
    {
        int Id { get; set; }
    }
}
