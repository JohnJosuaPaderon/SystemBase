namespace SystemBase.Security.Entities
{
    public partial class Permission
    {
        public sealed class SaveResult : SaveResultBase
        {
            public Permission Permission { get; set; }
        }
    }
}
