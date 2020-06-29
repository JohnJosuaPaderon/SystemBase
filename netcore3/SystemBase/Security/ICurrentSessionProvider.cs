using SystemBase.Security.Entities;

namespace SystemBase.Security
{
    public interface ICurrentSessionProvider
    {
        void Set(Session session);
        Session Get();
    }
}
