using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISearchPermission : IAsyncProcess<Permission.SearchResult>
    {
        Permission.SearchModel Model { get; set; }
    }
}
