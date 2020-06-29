using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISaveModule : IAsyncProcess<Module.SaveResult>
    {
        Module.SaveModel Model { get; set; }
    }
}
