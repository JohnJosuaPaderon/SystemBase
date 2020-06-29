using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISearchPermissionScope : IAsyncProcess<PermissionScope.SearchResult>
    {
        PermissionScope.SearchModel Model { get; set; }
    }
}
