using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISaveApplicationPermission : IAsyncProcess<ApplicationPermission.SaveResult>
    {
        ApplicationPermission.SaveModel Model { get; set; }
    }
}
