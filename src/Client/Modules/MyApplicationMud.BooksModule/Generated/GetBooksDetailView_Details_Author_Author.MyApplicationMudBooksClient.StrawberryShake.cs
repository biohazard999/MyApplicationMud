﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class GetBooksDetailView_Details_Author_Author : global::System.IEquatable<GetBooksDetailView_Details_Author_Author>, IGetBooksDetailView_Details_Author_Author
    {
        public GetBooksDetailView_Details_Author_Author(global::System.Int32 id, global::System.String name)
        {
            Id = id;
            Name = name;
        }

        public global::System.Int32 Id { get; }

        public global::System.String Name { get; }

        public virtual global::System.Boolean Equals(GetBooksDetailView_Details_Author_Author? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return (Id == other.Id) && Name.Equals(other.Name);
        }

        public override global::System.Boolean Equals(global::System.Object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((GetBooksDetailView_Details_Author_Author)obj);
        }

        public override global::System.Int32 GetHashCode()
        {
            unchecked
            {
                int hash = 5;
                hash ^= 397 * Id.GetHashCode();
                hash ^= 397 * Name.GetHashCode();
                return hash;
            }
        }
    }
}
