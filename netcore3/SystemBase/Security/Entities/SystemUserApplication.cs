namespace SystemBase.Security.Entities
{
    public class SystemUserApplication
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public bool IsApproved { get; set; }

        public SystemUser User { get; private set; }
        public SystemApplication Application { get; private set; }

        public static bool operator ==(SystemUserApplication left, SystemUserApplication right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SystemUserApplication left, SystemUserApplication right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            if (obj is SystemUserApplication value)
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
