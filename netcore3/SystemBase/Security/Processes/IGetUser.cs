using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IGetUser : IAsyncProcess<User>
    {
        int Id { get; set; }
    }
}
