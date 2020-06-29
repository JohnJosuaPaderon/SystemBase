namespace SystemBase.Security.Entities
{
    public partial class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PlatformId { get; set; }

        public Platform Platform { get; internal set; }

        public static bool operator ==(Application left, Application right) => Equals(left, right);

        public static bool operator !=(Application left, Application right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return (obj is Application value) && (!Equals(Id, default(int)) || !Equals(value.Id, default(int))) && Equals(Id, value.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Name;
    }
}
