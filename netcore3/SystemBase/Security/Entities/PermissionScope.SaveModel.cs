using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class PermissionScope
    {
        public sealed class SaveModel
        {
            public PermissionScope Scope { get; set; }
            public IList<Permission> Permissions { get; set; } = new List<Permission>();
            public IList<Permission.DeleteModel> DeletedPermissions { get; set; } = new List<Permission.DeleteModel>();
        }
    }
}
