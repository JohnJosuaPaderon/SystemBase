using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class User
    {
        public sealed class SearchModel
        {
            public string FilterText { get; set; }
            public bool? IsActive { get; set; }
            public bool? IsPasswordChangeRequired { get; set; }
            public IList<int> SkippedIds { get; set; } = new List<int>();
        }
    }
}
