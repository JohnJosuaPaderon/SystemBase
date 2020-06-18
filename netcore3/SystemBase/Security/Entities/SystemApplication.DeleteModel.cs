using System.Collections;

namespace SystemBase.Security.Entities
{
    public partial class SystemApplication
    {
        public sealed class DeleteModel
        {
            public int Id { get; set; }
            public bool IsCascaded { get; set; }
        }
    }
}
