using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class SystemApplication
    {
        public sealed class SearchResult : PaginationResult
        {
            public IList<SystemApplication> Applications { get; set; } = new List<SystemApplication>();
        }
    }
}
