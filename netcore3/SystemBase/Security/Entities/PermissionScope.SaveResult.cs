using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class PermissionScope
    {
        public sealed class SaveResult
        {
            public PermissionScope Scope { get; set; }
            public IList<Permission> Permissions { get; set; } = new List<Permission>();
            public IList<int> DeletedPermissionIds { get; set; } = new List<int>();
        }
    }
}
