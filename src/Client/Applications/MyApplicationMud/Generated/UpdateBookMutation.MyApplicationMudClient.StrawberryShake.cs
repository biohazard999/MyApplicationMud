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
    public partial class UpdateBookMutation : global::MyApplicationMud.GraphQL.IUpdateBookMutation
    {
        private readonly global::StrawberryShake.IOperationExecutor<IUpdateBookResult> _operationExecutor;
        private readonly global::StrawberryShake.Serialization.IInputValueFormatter _iDFormatter;
        private readonly global::StrawberryShake.Serialization.IInputValueFormatter _bookModelInputFormatter;
        public UpdateBookMutation(global::StrawberryShake.IOperationExecutor<IUpdateBookResult> operationExecutor, global::StrawberryShake.Serialization.ISerializerResolver serializerResolver)
        {
            _operationExecutor = operationExecutor ?? throw new global::System.ArgumentNullException(nameof(operationExecutor));
            _iDFormatter = serializerResolver.GetInputValueFormatter("ID");
            _bookModelInputFormatter = serializerResolver.GetInputValueFormatter("BookModelInput");
        }

        global::System.Type global::StrawberryShake.IOperationRequestFactory.ResultType => typeof(IUpdateBookResult);
        public async global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<IUpdateBookResult>> ExecuteAsync(global::System.String id, global::MyApplicationMud.GraphQL.BookModelInput book, global::System.Threading.CancellationToken cancellationToken = default)
        {
            var request = CreateRequest(id, book);
            return await _operationExecutor.ExecuteAsync(request, cancellationToken).ConfigureAwait(false);
        }

        public global::System.IObservable<global::StrawberryShake.IOperationResult<IUpdateBookResult>> Watch(global::System.String id, global::MyApplicationMud.GraphQL.BookModelInput book, global::StrawberryShake.ExecutionStrategy? strategy = null)
        {
            var request = CreateRequest(id, book);
            return _operationExecutor.Watch(request, strategy);
        }

        private global::StrawberryShake.OperationRequest CreateRequest(global::System.String id, global::MyApplicationMud.GraphQL.BookModelInput book)
        {
            var variables = new global::System.Collections.Generic.Dictionary<global::System.String, global::System.Object?>();
            variables.Add("id", FormatId(id));
            variables.Add("book", FormatBook(book));
            return CreateRequest(variables);
        }

        private global::StrawberryShake.OperationRequest CreateRequest(global::System.Collections.Generic.IReadOnlyDictionary<global::System.String, global::System.Object?>? variables)
        {
            return new global::StrawberryShake.OperationRequest(id: UpdateBookMutationDocument.Instance.Hash.Value, name: "UpdateBook", document: UpdateBookMutationDocument.Instance, strategy: global::StrawberryShake.RequestStrategy.Default, variables: variables);
        }

        private global::System.Object? FormatId(global::System.String value)
        {
            if (value is null)
            {
                throw new global::System.ArgumentNullException(nameof(value));
            }

            return _iDFormatter.Format(value);
        }

        private global::System.Object? FormatBook(global::MyApplicationMud.GraphQL.BookModelInput value)
        {
            if (value is null)
            {
                throw new global::System.ArgumentNullException(nameof(value));
            }

            return _bookModelInputFormatter.Format(value);
        }

        global::StrawberryShake.OperationRequest global::StrawberryShake.IOperationRequestFactory.Create(global::System.Collections.Generic.IReadOnlyDictionary<global::System.String, global::System.Object?>? variables)
        {
            return CreateRequest(variables!);
        }
    }
}