﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    /// <summary>
    /// Represents the operation service of the UpdateBook GraphQL operation
    /// <code>
    /// mutation UpdateBook($bookId: Int!, $book: EditBookInput!) {
    ///   editBook(bookId: $bookId, book: $book) {
    ///     __typename
    ///     ... BookInfo
    ///     ... on Book {
    ///       id
    ///     }
    ///   }
    /// }
    /// 
    /// fragment BookInfo on Book {
    ///   id
    ///   title
    ///   image
    ///   author {
    ///     __typename
    ///     ... AuthorInfo
    ///     ... on Author {
    ///       id
    ///     }
    ///   }
    /// }
    /// 
    /// fragment AuthorInfo on Author {
    ///   id
    ///   name
    /// }
    /// </code>
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial interface IUpdateBookMutation : global::StrawberryShake.IOperationRequestFactory
    {
        global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<IUpdateBookResult>> ExecuteAsync(global::System.Int32 bookId, global::MyApplicationMud.GraphQL.EditBookInput book, global::System.Threading.CancellationToken cancellationToken = default);
        global::System.IObservable<global::StrawberryShake.IOperationResult<IUpdateBookResult>> Watch(global::System.Int32 bookId, global::MyApplicationMud.GraphQL.EditBookInput book, global::StrawberryShake.ExecutionStrategy? strategy = null);
    }
}