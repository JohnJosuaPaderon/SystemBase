using Sorschia.Processes;
using SystemBase.Security.Entities;

namespace SystemBase.Security.Processes
{
    public interface ISearchModule : IAsyncProcess<Module.SearchResult>
    {
        Module.SearchModel Model { get; set; }
    }
}
