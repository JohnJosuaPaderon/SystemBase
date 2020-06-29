using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISearchApplication : IAsyncProcess<Application.SearchResult>
    {
        Application.SearchModel Model { get; set; }
    }
}
