using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface IDeleteUser : IAsyncProcess<bool>
    {
        User.DeleteModel Model { get; set; }
    }
}
