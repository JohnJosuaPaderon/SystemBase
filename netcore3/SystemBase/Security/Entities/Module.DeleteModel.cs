namespace SystemBase.Security.Entities
{
    public partial class Module
    {
        public sealed class DeleteModel
        {
            public int Id { get; set; }
            public bool IsCascaded { get; set; }
        }
    }
}
