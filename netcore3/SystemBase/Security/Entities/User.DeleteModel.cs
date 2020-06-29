namespace SystemBase.Security.Entities
{
    public partial class User
    {
        public sealed class DeleteModel
        {
            public int Id { get; set; }
            public bool IsCacaded { get; set; }
        }
    }
}
