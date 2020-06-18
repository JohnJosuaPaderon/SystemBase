using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class SystemApplication
    {
        public sealed class SaveResult
        {
            public SystemApplication Application { get; set; }
            public IList<SystemUserApplication> UserApplications { get; set; } = new List<SystemUserApplication>();
            public IList<SystemModule> Modules { get; set; } = new List<SystemModule>();
            public IList<long> DeletedUserApplicationIds { get; set; } = new List<long>();
            public IList<int> RemovedModuleIds { get; set; } = new List<int>();
        }
    }
}
