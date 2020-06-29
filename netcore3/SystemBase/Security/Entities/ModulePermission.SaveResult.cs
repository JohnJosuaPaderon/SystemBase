namespace SystemBase.Security.Entities
{
    public partial class ModulePermission
    {
        public new sealed class SaveResult : SaveResultBase
        {
            public ModulePermission Permission { get; set; }
        }
    }
}
