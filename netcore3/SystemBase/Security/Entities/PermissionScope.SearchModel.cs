﻿using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class PermissionScope
    {
        public sealed class SearchModel : PaginationModel
        {
            public string FilterText { get; set; }
            public IList<int> SkippedIds { get; set; } = new List<int>();
        }
    }
}
