using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Application
    {
        public sealed class SearchResult : PaginationResult
        {
            public IList<Application> Applications { get; set; } = new List<Application>();
        }
    }
}
