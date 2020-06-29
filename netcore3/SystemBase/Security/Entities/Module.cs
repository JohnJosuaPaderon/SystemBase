namespace SystemBase.Security.Entities
{
    public partial class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrdinalNumber { get; set; }
        public int? ApplicationId { get; set; }

        public Application Application { get; internal set; }

        public static bool operator ==(Module left, Module right) => Equals(left, right);

        public static bool operator !=(Module left, Module right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return (obj is Module value) && (!Equals(Id, default(int)) || !Equals(value.Id, default(int))) && Equals(Id, value.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Name;
    }
}
