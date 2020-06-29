namespace SystemBase.Security.Entities
{
    public partial class ModulePermission : Permission
    {
        public int? ModuleId { get; set; }

        public Module Module { get; internal set; }
    }
}
