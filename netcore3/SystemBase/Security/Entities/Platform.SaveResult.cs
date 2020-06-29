using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Platform
    {
        public sealed class SaveResult
        {
            public Platform Platform { get; set; }
            public IList<Application> Applications { get; set; } = new List<Application>();
            public IList<int> DeletedApplicationIds { get; set; } = new List<int>();
        }
    }
}
