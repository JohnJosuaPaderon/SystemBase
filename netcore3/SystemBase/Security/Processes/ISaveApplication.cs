using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISaveApplication : IAsyncProcess<Application.SaveResult>
    {
        Application.SaveModel Model { get; set; }
    }
}
