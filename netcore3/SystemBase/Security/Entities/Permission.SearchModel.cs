using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Permission
    {
        public sealed class SearchModel : PaginationModel
        {
            public string FilterText { get; set; }
            public bool FilterByScope { get; set; }
            public IList<int> ScopeIds { get; set; } = new List<int>();
            public IList<int> SkippedIds { get; set; } = new List<int>();
        }
    }
}
