﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class GetBookAuthorsResult : global::System.IEquatable<GetBookAuthorsResult>, IGetBookAuthorsResult
    {
        public GetBookAuthorsResult(global::System.Collections.Generic.IReadOnlyList<global::MyApplicationMud.GraphQL.IGetBookAuthors_Authors> authors)
        {
            Authors = authors;
        }

        public global::System.Collections.Generic.IReadOnlyList<global::MyApplicationMud.GraphQL.IGetBookAuthors_Authors> Authors { get; }

        public virtual global::System.Boolean Equals(GetBookAuthorsResult? other)
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

            return (global::StrawberryShake.Helper.ComparisonHelper.SequenceEqual(Authors, other.Authors));
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

            return Equals((GetBookAuthorsResult)obj);
        }

        public override global::System.Int32 GetHashCode()
        {
            unchecked
            {
                int hash = 5;
                foreach (var Authors_elm in Authors)
                {
                    hash ^= 397 * Authors_elm.GetHashCode();
                }

                return hash;
            }
        }
    }
}