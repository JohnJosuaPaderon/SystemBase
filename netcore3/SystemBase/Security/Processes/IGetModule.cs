using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IGetModule : IAsyncProcess<Module>
    {
        int Id { get; set; }
    }
}
