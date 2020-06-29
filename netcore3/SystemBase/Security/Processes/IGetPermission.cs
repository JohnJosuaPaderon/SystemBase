using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IGetPermission : IAsyncProcess<Permission>
    {
        int Id { get; set; }
    }
}
