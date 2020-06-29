using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IDeleteModule : IAsyncProcess<bool>
    {
        Module.DeleteModel Model { get; set; }
    }
}
