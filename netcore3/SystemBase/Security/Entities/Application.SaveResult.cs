using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Application
    {
        public sealed class SaveResult
        {
            public Application Application { get; set; }
            public IList<ApplicationPermission> Permissions { get; set; } = new List<ApplicationPermission>();
            public IList<Module> Modules { get; set; } = new List<Module>();
            public IList<UserApplication> UserApplications { get; set; } = new List<UserApplication>();
            public IList<int> DeletedPermissionIds { get; set; } = new List<int>();
            public IList<int> DeletedModuleIds { get; set; } = new List<int>();
            public IList<long> DeletedUserApplicationIds { get; set; } = new List<long>();
        }
    }
}
