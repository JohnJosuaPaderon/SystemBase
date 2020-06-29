namespace SystemBase.Security.Entities
{
    public partial class PermissionScope
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static bool operator ==(PermissionScope left, PermissionScope right) => Equals(left, right);

        public static bool operator !=(PermissionScope left, PermissionScope right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return (obj is PermissionScope value) && (!Equals(Id, default(int)) || !Equals(value.Id, default(int))) && Equals(Id, value.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Name;
    }
}
