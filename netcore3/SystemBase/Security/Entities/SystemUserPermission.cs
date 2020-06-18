namespace SystemBase.Security.Entities
{
    public class SystemUserPermission
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int PermissionId { get; set; }
        public bool IsApproved { get; set; }

        public SystemUser User { get; private set; }
        public SystemPermission Permission { get; private set; }

        public static bool operator ==(SystemUserPermission left, SystemUserPermission right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SystemUserPermission left, SystemUserPermission right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            if (obj is SystemUserPermission value)
            {
                return (Equals(Id, default(long)) && Equals(value.Id, default(long))) ? false : Equals(Id, value.Id);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
