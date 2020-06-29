namespace SystemBase.Security.Entities
{
    public partial class ModulePermission
    {
        public new sealed class SaveModel : SaveModelBase
        {
            public ModulePermission Permission { get; set; }
        }
    }
}
