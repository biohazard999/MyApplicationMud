﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    /// <summary>
    /// Represents the operation service of the GetBooksListView GraphQL operation
    /// <code>
    /// query GetBooksListView {
    ///   items: books {
    ///     __typename
    ///     id
    ///     title
    ///     image
    ///     author {
    ///       __typename
    ///       name
    ///       ... on Author {
    ///         id
    ///       }
    ///     }
    ///     ... on Book {
    ///       id
    ///     }
    ///   }
    /// }
    /// </code>
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class GetBooksListViewQueryDocument : global::StrawberryShake.IDocument
    {
        private GetBooksListViewQueryDocument()
        {
        }

        public static GetBooksListViewQueryDocument Instance { get; } = new GetBooksListViewQueryDocument();
        public global::StrawberryShake.OperationKind Kind => global::StrawberryShake.OperationKind.Query;
        public global::System.ReadOnlySpan<global::System.Byte> Body => new global::System.Byte[]{0x71, 0x75, 0x65, 0x72, 0x79, 0x20, 0x47, 0x65, 0x74, 0x42, 0x6f, 0x6f, 0x6b, 0x73, 0x4c, 0x69, 0x73, 0x74, 0x56, 0x69, 0x65, 0x77, 0x20, 0x7b, 0x20, 0x69, 0x74, 0x65, 0x6d, 0x73, 0x3a, 0x20, 0x62, 0x6f, 0x6f, 0x6b, 0x73, 0x20, 0x7b, 0x20, 0x5f, 0x5f, 0x74, 0x79, 0x70, 0x65, 0x6e, 0x61, 0x6d, 0x65, 0x20, 0x69, 0x64, 0x20, 0x74, 0x69, 0x74, 0x6c, 0x65, 0x20, 0x69, 0x6d, 0x61, 0x67, 0x65, 0x20, 0x61, 0x75, 0x74, 0x68, 0x6f, 0x72, 0x20, 0x7b, 0x20, 0x5f, 0x5f, 0x74, 0x79, 0x70, 0x65, 0x6e, 0x61, 0x6d, 0x65, 0x20, 0x6e, 0x61, 0x6d, 0x65, 0x20, 0x2e, 0x2e, 0x2e, 0x20, 0x6f, 0x6e, 0x20, 0x41, 0x75, 0x74, 0x68, 0x6f, 0x72, 0x20, 0x7b, 0x20, 0x69, 0x64, 0x20, 0x7d, 0x20, 0x7d, 0x20, 0x2e, 0x2e, 0x2e, 0x20, 0x6f, 0x6e, 0x20, 0x42, 0x6f, 0x6f, 0x6b, 0x20, 0x7b, 0x20, 0x69, 0x64, 0x20, 0x7d, 0x20, 0x7d, 0x20, 0x7d};
        public global::StrawberryShake.DocumentHash Hash { get; } = new global::StrawberryShake.DocumentHash("md5Hash", "1eaec64d0b6a82c11fb8d46fd6e0a7a8");
        public override global::System.String ToString()
        {
#if NETSTANDARD2_0
        return global::System.Text.Encoding.UTF8.GetString(Body.ToArray());
#else
            return global::System.Text.Encoding.UTF8.GetString(Body);
#endif
        }
    }
}