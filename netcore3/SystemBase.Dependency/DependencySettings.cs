using SystemBase.Security;

namespace SystemBase
{
    public sealed class DependencySettings
    {
        public SecurityDependencySettings Security { get; } = new SecurityDependencySettings();

        internal DependencySettings() { }
    }
}
