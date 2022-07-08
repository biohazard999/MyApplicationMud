﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class AddBook_AddBook_Book : global::System.IEquatable<AddBook_AddBook_Book>, IAddBook_AddBook_Book
    {
        public AddBook_AddBook_Book(global::System.Int32 id, global::System.String title, global::System.String image, global::MyApplicationMud.GraphQL.IGetBooksDetailView_Details_Author author)
        {
            Id = id;
            Title = title;
            Image = image;
            Author = author;
        }

        public global::System.Int32 Id { get; }

        public global::System.String Title { get; }

        public global::System.String Image { get; }

        public global::MyApplicationMud.GraphQL.IGetBooksDetailView_Details_Author Author { get; }

        public virtual global::System.Boolean Equals(AddBook_AddBook_Book? other)
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

            return (Id == other.Id) && Title.Equals(other.Title) && Image.Equals(other.Image) && Author.Equals(other.Author);
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

            return Equals((AddBook_AddBook_Book)obj);
        }

        public override global::System.Int32 GetHashCode()
        {
            unchecked
            {
                int hash = 5;
                hash ^= 397 * Id.GetHashCode();
                hash ^= 397 * Title.GetHashCode();
                hash ^= 397 * Image.GetHashCode();
                hash ^= 397 * Author.GetHashCode();
                return hash;
            }
        }
    }
}