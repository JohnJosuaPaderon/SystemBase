using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Module
    {
        public sealed class SearchResult : PaginationResult
        {
            public IList<Module> Modules { get; set; } = new List<Module>();
        }
    }
}
