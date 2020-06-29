using System.Collections;

namespace SystemBase.Security.Entities
{
    public partial class Application
    {
        public sealed class DeleteModel
        {
            public int Id { get; set; }
            public bool IsCascaded { get; set; }
        }
    }
}
