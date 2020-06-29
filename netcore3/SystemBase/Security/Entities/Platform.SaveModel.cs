using System.Collections.Generic;

namespace SystemBase.Security.Entities
{
    public partial class Platform
    {
        public sealed class SaveModel
        {
            public Platform Platform { get; set; }
            public IList<Application> Applications { get; set; } = new List<Application>();
            public IList<Application.DeleteModel> DeletedApplications { get; set; } = new List<Application.DeleteModel>();
        }
    }
}
