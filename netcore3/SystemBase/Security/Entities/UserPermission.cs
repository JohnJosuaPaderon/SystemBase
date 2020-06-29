namespace SystemBase.Security.Entities
{
    public class UserPermission
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int PermissionId { get; set; }
        public bool IsApproved { get; set; }

        public User User { get; internal set; }
        public Permission Permission { get; internal set; }

        public static bool operator ==(UserPermission left, UserPermission right) => Equals(left, right);

        public static bool operator !=(UserPermission left, UserPermission right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return (obj is UserPermission value) && (!Equals(Id, default(long)) || !Equals(value.Id, default(long))) && Equals(Id, value.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
