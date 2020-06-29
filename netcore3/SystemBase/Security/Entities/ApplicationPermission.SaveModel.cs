namespace SystemBase.Security.Entities
{
    public partial class ApplicationPermission
    {
        public new sealed class SaveModel : SaveModelBase
        {
            public ApplicationPermission Permission { get; set; }
        }
    }
}
