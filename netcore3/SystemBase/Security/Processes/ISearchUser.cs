using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISearchUser : IAsyncProcess<User.SearchResult>
    {
        User.SearchModel Model { get; set; }
    }
}
