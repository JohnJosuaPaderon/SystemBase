namespace SystemBase.Security.Configuration
{
    public sealed partial class SecurityConfiguration
    {
        public sealed partial class CacheExpirationSection
        {
            public ApplicationSection Application { get; set; } = new ApplicationSection();
            public ModuleSection Module { get; set; } = new ModuleSection();
            public PermissionSection Permission { get; set; } = new PermissionSection();
            public PermissionScopeSection PermissionScope { get; set; } = new PermissionScopeSection();
            public PlatformSection Platform { get; set; } = new PlatformSection();
            public UserSection User { get; set; } = new UserSection();
        }
    }
}
