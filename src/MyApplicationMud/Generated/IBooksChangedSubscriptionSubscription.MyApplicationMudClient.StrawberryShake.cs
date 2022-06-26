﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    /// <summary>
    /// Represents the operation service of the BooksChangedSubscription GraphQL operation
    /// <code>
    /// subscription BooksChangedSubscription {
    ///   changed: bookChanged {
    ///     __typename
    ///     type
    ///     book {
    ///       __typename
    ///       ... BookListInfo
    ///       ... on Book {
    ///         id
    ///       }
    ///     }
    ///   }
    /// }
    /// 
    /// fragment BookListInfo on Book {
    ///   id
    ///   title
    ///   image
    ///   author {
    ///     __typename
    ///     name
    ///     ... on Author {
    ///       id
    ///     }
    ///   }
    /// }
    /// </code>
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial interface IBooksChangedSubscriptionSubscription : global::StrawberryShake.IOperationRequestFactory
    {
        global::System.IObservable<global::StrawberryShake.IOperationResult<IBooksChangedSubscriptionResult>> Watch(global::StrawberryShake.ExecutionStrategy? strategy = null);
    }
}
