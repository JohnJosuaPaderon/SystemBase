using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Permission
    {
        public abstract class SaveResultBase
        {
            public IList<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
            public IList<long> DeletedUserPermissionIds { get; set; } = new List<long>();
        }
    }
}
