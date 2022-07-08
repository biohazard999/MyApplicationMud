﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    /// <summary>
    /// Represents the operation service of the UpdateBook GraphQL operation
    /// <code>
    /// mutation UpdateBook($id: ID!, $book: BookModelInput!) {
    ///   editBook(bookId: $id, book: $book) {
    ///     __typename
    ///     errors {
    ///       __typename
    ///       ... ValidationError
    ///     }
    ///     value {
    ///       __typename
    ///       ... BookInfo
    ///       ... on Book {
    ///         id
    ///       }
    ///     }
    ///   }
    /// }
    /// 
    /// fragment ValidationError on ValidationError {
    ///   propertyName
    ///   message
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
        global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<IUpdateBookResult>> ExecuteAsync(global::System.String id, global::MyApplicationMud.GraphQL.BookModelInput book, global::System.Threading.CancellationToken cancellationToken = default);
        global::System.IObservable<global::StrawberryShake.IOperationResult<IUpdateBookResult>> Watch(global::System.String id, global::MyApplicationMud.GraphQL.BookModelInput book, global::StrawberryShake.ExecutionStrategy? strategy = null);
    }
}