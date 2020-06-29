namespace SystemBase.Security
{
    public sealed class SecurityDependencySettings
    {
        public bool UseRepositoryCaching { get; set; } = true;

        internal SecurityDependencySettings() { }
    }
}
