using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class User
    {
        public sealed class SaveResult
        {
            public User User { get; set; }
            public IList<UserApplication> UserApplications { get; set; } = new List<UserApplication>();
            public IList<UserModule> UserModules { get; set; } = new List<UserModule>();
            public IList<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
            public IList<long> DeletedUserApplicationIds { get; set; } = new List<long>();
            public IList<long> DeletedUserModuleIds { get; set; } = new List<long>();
            public IList<long> DeletedUserPermissionIds { get; set; } = new List<long>();
        }
    }
}
