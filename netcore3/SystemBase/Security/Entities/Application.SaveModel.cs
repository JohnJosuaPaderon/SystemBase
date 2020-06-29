using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Application
    {
        public sealed class SaveModel
        {
            public Application Application { get; set; }
            public IList<ApplicationPermission> Permissions { get; set; } = new List<ApplicationPermission>();
            public IList<Module> Modules { get; set; } = new List<Module>();
            public IList<UserApplication> UserApplications { get; set; } = new List<UserApplication>();
            public IList<Permission.DeleteModel> DeletedPermissions { get; set; } = new List<Permission.DeleteModel>();
            public IList<Module.DeleteModel> DeletedModules { get; set; } = new List<Module.DeleteModel>();
            public IList<long> DeletedUserApplicationIds { get; set; } = new List<long>();
        }
    }
}
