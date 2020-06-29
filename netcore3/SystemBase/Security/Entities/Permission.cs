namespace SystemBase.Security.Entities
{
    public partial class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ScopeId { get; set; }

        public PermissionScope Scope { get; internal set; }

        public static bool operator ==(Permission left, Permission right) => Equals(left, right);

        public static bool operator !=(Permission left, Permission right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return (obj is Permission value) && (!Equals(Id, default(int)) || !Equals(value.Id, default(int))) && Equals(Id, value.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Name;
    }
}
