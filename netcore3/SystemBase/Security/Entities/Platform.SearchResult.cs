using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Platform
    {
        public sealed class SearchResult : PaginationResult
        {
            public IList<Platform> ApplicationPlatforms { get; set; } = new List<Platform>();
        }
    }
}
