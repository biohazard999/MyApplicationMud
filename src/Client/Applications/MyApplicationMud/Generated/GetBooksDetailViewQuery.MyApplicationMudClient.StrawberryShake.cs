﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    /// <summary>
    /// Represents the operation service of the GetBooksDetailView GraphQL operation
    /// <code>
    /// query GetBooksDetailView($id: Int!) {
    ///   details: book(id: $id) {
    ///     __typename
    ///     ... BookInfo
    ///     ... on Book {
    ///       id
    ///     }
    ///   }
    ///   authors: authors {
    ///     __typename
    ///     ... AuthorInfo
    ///     ... on Author {
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
    public partial class GetBooksDetailViewQuery : global::MyApplicationMud.GraphQL.IGetBooksDetailViewQuery
    {
        private readonly global::StrawberryShake.IOperationExecutor<IGetBooksDetailViewResult> _operationExecutor;
        private readonly global::StrawberryShake.Serialization.IInputValueFormatter _intFormatter;
        public GetBooksDetailViewQuery(global::StrawberryShake.IOperationExecutor<IGetBooksDetailViewResult> operationExecutor, global::StrawberryShake.Serialization.ISerializerResolver serializerResolver)
        {
            _operationExecutor = operationExecutor ?? throw new global::System.ArgumentNullException(nameof(operationExecutor));
            _intFormatter = serializerResolver.GetInputValueFormatter("Int");
        }

        global::System.Type global::StrawberryShake.IOperationRequestFactory.ResultType => typeof(IGetBooksDetailViewResult);
        public async global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<IGetBooksDetailViewResult>> ExecuteAsync(global::System.Int32 id, global::System.Threading.CancellationToken cancellationToken = default)
        {
            var request = CreateRequest(id);
            return await _operationExecutor.ExecuteAsync(request, cancellationToken).ConfigureAwait(false);
        }

        public global::System.IObservable<global::StrawberryShake.IOperationResult<IGetBooksDetailViewResult>> Watch(global::System.Int32 id, global::StrawberryShake.ExecutionStrategy? strategy = null)
        {
            var request = CreateRequest(id);
            return _operationExecutor.Watch(request, strategy);
        }

        private global::StrawberryShake.OperationRequest CreateRequest(global::System.Int32 id)
        {
            var variables = new global::System.Collections.Generic.Dictionary<global::System.String, global::System.Object?>();
            variables.Add("id", FormatId(id));
            return CreateRequest(variables);
        }

        private global::StrawberryShake.OperationRequest CreateRequest(global::System.Collections.Generic.IReadOnlyDictionary<global::System.String, global::System.Object?>? variables)
        {
            return new global::StrawberryShake.OperationRequest(id: GetBooksDetailViewQueryDocument.Instance.Hash.Value, name: "GetBooksDetailView", document: GetBooksDetailViewQueryDocument.Instance, strategy: global::StrawberryShake.RequestStrategy.Default, variables: variables);
        }

        private global::System.Object? FormatId(global::System.Int32 value)
        {
            return _intFormatter.Format(value);
        }

        global::StrawberryShake.OperationRequest global::StrawberryShake.IOperationRequestFactory.Create(global::System.Collections.Generic.IReadOnlyDictionary<global::System.String, global::System.Object?>? variables)
        {
            return CreateRequest(variables!);
        }
    }
}