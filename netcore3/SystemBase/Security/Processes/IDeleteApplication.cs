using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IDeleteApplication : IAsyncProcess<bool>
    {
        Application.DeleteModel Model { get; set; }
    }
}
