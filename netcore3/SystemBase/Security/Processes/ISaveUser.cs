using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISaveUser : IAsyncProcess<User.SaveResult>
    {
        User.SaveModel Model { get; set; }
    }
}
