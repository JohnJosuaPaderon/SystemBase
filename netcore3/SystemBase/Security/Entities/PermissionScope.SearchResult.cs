using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class PermissionScope
    {
        public sealed class SearchResult : PaginationResult
        {
            public IList<PermissionScope> Scopes { get; set; } = new List<PermissionScope>();
        }
    }
}
