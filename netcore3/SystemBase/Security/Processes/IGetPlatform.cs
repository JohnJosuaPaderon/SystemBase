using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IGetPlatform : IAsyncProcess<Platform>
    {
        int Id { get; set; }
    }
}
