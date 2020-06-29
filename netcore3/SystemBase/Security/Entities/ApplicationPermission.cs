namespace SystemBase.Security.Entities
{
    public partial class ApplicationPermission : Permission
    {
        public int? ApplicationId { get; set; }

        public Application Application { get; internal set; }
    }
}
