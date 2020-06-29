using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class User
    {
        public sealed class SearchResult
        {
            public IList<User> Users { get; set; } = new List<User>();
        }
    }
}
