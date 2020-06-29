namespace SystemBase.Security.Entities
{
    public partial class Permission
    {
        public sealed class SaveModel : SaveModelBase
        {
            public Permission Permission { get; set; }
        }
    }
}
