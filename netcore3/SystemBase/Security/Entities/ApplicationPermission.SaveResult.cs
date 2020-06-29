namespace SystemBase.Security.Entities
{
    public partial class ApplicationPermission
    {
        public new sealed class SaveResult : SaveResultBase
        {
            public ApplicationPermission Permission { get; set; }
        }
    }
}
