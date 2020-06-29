using System;

namespace SystemBase.Security
{
    public static class CurrentSessionProviderExtensions
    {
        public static Guid? GetId(this ICurrentSessionProvider instance) => instance?.Get()?.Id;
    }
}
