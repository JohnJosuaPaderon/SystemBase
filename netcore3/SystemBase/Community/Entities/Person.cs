using Sorschia.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBase.Community.Entities
{
    public class Person : NameBase
    {
        public int Id { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? SexId { get; set; }

        public Sex Sex { get; internal set; }

        public static bool operator ==(Person left, Person right) => Equals(left, right);

        public static bool operator !=(Person left, Person right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return (obj is Person value) && (!Equals(Id, default(int)) || !Equals(value.Id, default(int))) && Equals(Id, value.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => FullName;
    }
}
