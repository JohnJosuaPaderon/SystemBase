using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class User
    {
        public sealed class SearchResult : PaginationResult
        {
            public IList<User> Users { get; set; } = new List<User>();
        }
    }
}
