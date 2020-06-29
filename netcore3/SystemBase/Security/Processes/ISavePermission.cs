using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISavePermission : IAsyncProcess<Permission.SaveResult>
    {
        Permission.SaveModel Model { get; set; }
    }
}
