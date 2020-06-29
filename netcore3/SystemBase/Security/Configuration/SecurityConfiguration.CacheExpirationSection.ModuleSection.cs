namespace SystemBase.Security.Configuration
{
    public sealed partial class SecurityConfiguration
    {
        public sealed partial class CacheExpirationSection
        {
            public sealed class ModuleSection
            {
                public long? Delete { get; set; } = 60;
                public long? Get { get; set; } = 5;
                public long? Save { get; set; } = 10;
                public long? Search { get; set; } = 5;
            }
        }
    }
}
