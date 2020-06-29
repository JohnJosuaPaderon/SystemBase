namespace SystemBase.Security.Entities
{
    public partial class Platform
    {
        public sealed class DeleteModel
        {
            public int Id { get; set; }
            public bool IsCascaded { get; set; }
        }
    }
}
