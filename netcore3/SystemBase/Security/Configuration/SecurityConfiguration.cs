namespace SystemBase.Security.Configuration
{
    public sealed partial class SecurityConfiguration
    {
        public CacheExpirationSection CacheExpiration { get; set; } = new CacheExpirationSection();
    }
}
