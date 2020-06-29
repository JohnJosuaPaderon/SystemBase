using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IGetApplication : IAsyncProcess<Application>
    {
        int Id { get; set; }
    }
}
