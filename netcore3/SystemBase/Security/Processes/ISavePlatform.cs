using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISavePlatform : IAsyncProcess<Platform.SaveResult>
    {
        Platform.SaveModel Model { get; set; }
    }
}
