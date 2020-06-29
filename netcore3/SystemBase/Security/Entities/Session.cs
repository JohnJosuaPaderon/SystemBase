using System;

namespace SystemBase.Security.Entities
{
    public class Session
    {
        public Guid Id { get; set; }
        public int? UserId { get; set; }
        public int? ApplicationId { get; set; }
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public string Operatingsystem { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }

        public Application Application { get; internal set; }
        public User User { get; internal set; }

        public static bool operator ==(Session left, Session right) => Equals(left, right);

        public static bool operator !=(Session left, Session right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return (obj is Session value) && (!Equals(Id, default(Guid)) || !Equals(value.Id, default(Guid))) && Equals(Id, value.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
