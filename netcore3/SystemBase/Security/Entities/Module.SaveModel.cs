using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Module
    {
        public sealed class SaveModel
        {
            public Module Module { get; set; }
            public IList<ModulePermission> Permissions { get; set; } = new List<ModulePermission>();
            public IList<UserModule> UserModules { get; set; } = new List<UserModule>();
            public IList<Permission.DeleteModel> DeletedPermissions { get; set; } = new List<Permission.DeleteModel>();
            public IList<long> DeletedUserModuleIds { get; set; } = new List<long>();
        }
    }
}
