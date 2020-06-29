namespace SystemBase.Security.Entities
{
    public class UserModule
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int ModuleId { get; set; }
        public bool IsApproved { get; set; }

        public User User { get; internal set; }
        public Module Module { get; internal set; }

        public static bool operator ==(UserModule left, UserModule right) => Equals(left, right);

        public static bool operator !=(UserModule left, UserModule right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return (obj is UserModule value) && (!Equals(Id, default(long)) || !Equals(value.Id, default(long))) && Equals(Id, value.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
