﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class BooksSubscriptionResult : global::System.IEquatable<BooksSubscriptionResult>, IBooksSubscriptionResult
    {
        public BooksSubscriptionResult(global::MyApplicationMud.GraphQL.IBooksSubscription_Changed changed)
        {
            Changed = changed;
        }

        public global::MyApplicationMud.GraphQL.IBooksSubscription_Changed Changed { get; }

        public virtual global::System.Boolean Equals(BooksSubscriptionResult? other)
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

            return (Changed.Equals(other.Changed));
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

            return Equals((BooksSubscriptionResult)obj);
        }

        public override global::System.Int32 GetHashCode()
        {
            unchecked
            {
                int hash = 5;
                hash ^= 397 * Changed.GetHashCode();
                return hash;
            }
        }
    }
}
