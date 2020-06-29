using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Application
    {
        public sealed class SearchModel : PaginationModel
        {
            public string FilterText { get; set; }
            public bool FilterByPlatform { get; set; }
            public IList<int> PlatformIds { get; set; } = new List<int>();
            public IList<int> SkippedIds { get; set; } = new List<int>();
        }
    }
}
