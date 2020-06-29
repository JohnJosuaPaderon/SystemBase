using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Module
    {
        public sealed class SaveResult
        {
            public Module Module { get; set; }
            public IList<ModulePermission> Permissions { get; set; } = new List<ModulePermission>();
            public IList<UserModule> UserModules { get; set; } = new List<UserModule>();
            public IList<int> DeletedPermissionIds { get; set; } = new List<int>();
            public IList<long> DeletedUserModuleIds { get; set; } = new List<long>();
            public IList<int> RemovedPermissionIds { get; set; } = new List<int>();
        }
    }
}
