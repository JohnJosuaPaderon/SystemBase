using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IDeletePlatform : IAsyncProcess<bool>
    {
        Platform.DeleteModel Model { get; set; }
    }
}
