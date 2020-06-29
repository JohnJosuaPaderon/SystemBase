using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IDeletePermission : IAsyncProcess<bool>
    {
        Permission.DeleteModel Model { get; set; }
    }
}
