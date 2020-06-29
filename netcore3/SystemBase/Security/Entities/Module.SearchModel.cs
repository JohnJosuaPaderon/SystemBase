using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Module
    {
        public sealed class SearchModel : PaginationModel
        {
            public string FilterText { get; set; }
            public bool FilterByApplication { get; set; }
            public IList<int> ApplicationIds { get; set; } = new List<int>();
            public IList<int> SkippedIds { get; set; } = new List<int>();
        }
    }
}
