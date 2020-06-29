using System;

namespace SystemBase.Security
{
    public class AccessToken
    {
        public Guid Id { get; set; }
        public Guid? SessionId { get; set; }
        public string TokenString { get; set; }
        public DateTime Expiration { get; set; }

        public static bool operator ==(AccessToken left, AccessToken right) => Equals(left, right);

        public static bool operator !=(AccessToken left, AccessToken right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return (obj is AccessToken value) && (!Equals(Id, default(Guid)) || !Equals(value.Id, default(Guid))) && Equals(Id, value.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
