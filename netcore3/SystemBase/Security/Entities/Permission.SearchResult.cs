using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Permission
    {
        public sealed class SearchResult : PaginationResult
        {
            public IList<Permission> Permissions { get; set; } = new List<Permission>();
        }
    }
}
