using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISearchPlatform : IAsyncProcess<Platform.SearchResult>
    {
        Platform.SearchModel Model { get; set; }
    }
}
