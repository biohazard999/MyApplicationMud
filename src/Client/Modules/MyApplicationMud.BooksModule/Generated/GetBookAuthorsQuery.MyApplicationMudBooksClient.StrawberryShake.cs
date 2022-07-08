﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    /// <summary>
    /// Represents the operation service of the GetBookAuthors GraphQL operation
    /// <code>
    /// query GetBookAuthors {
    ///   authors: authors {
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
    public partial class GetBookAuthorsQuery : global::MyApplicationMud.GraphQL.IGetBookAuthorsQuery
    {
        private readonly global::StrawberryShake.IOperationExecutor<IGetBookAuthorsResult> _operationExecutor;
        public GetBookAuthorsQuery(global::StrawberryShake.IOperationExecutor<IGetBookAuthorsResult> operationExecutor)
        {
            _operationExecutor = operationExecutor ?? throw new global::System.ArgumentNullException(nameof(operationExecutor));
        }

        global::System.Type global::StrawberryShake.IOperationRequestFactory.ResultType => typeof(IGetBookAuthorsResult);
        public async global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<IGetBookAuthorsResult>> ExecuteAsync(global::System.Threading.CancellationToken cancellationToken = default)
        {
            var request = CreateRequest();
            return await _operationExecutor.ExecuteAsync(request, cancellationToken).ConfigureAwait(false);
        }

        public global::System.IObservable<global::StrawberryShake.IOperationResult<IGetBookAuthorsResult>> Watch(global::StrawberryShake.ExecutionStrategy? strategy = null)
        {
            var request = CreateRequest();
            return _operationExecutor.Watch(request, strategy);
        }

        private global::StrawberryShake.OperationRequest CreateRequest()
        {
            return CreateRequest(null);
        }

        private global::StrawberryShake.OperationRequest CreateRequest(global::System.Collections.Generic.IReadOnlyDictionary<global::System.String, global::System.Object?>? variables)
        {
            return new global::StrawberryShake.OperationRequest(id: GetBookAuthorsQueryDocument.Instance.Hash.Value, name: "GetBookAuthors", document: GetBookAuthorsQueryDocument.Instance, strategy: global::StrawberryShake.RequestStrategy.Default);
        }

        global::StrawberryShake.OperationRequest global::StrawberryShake.IOperationRequestFactory.Create(global::System.Collections.Generic.IReadOnlyDictionary<global::System.String, global::System.Object?>? variables)
        {
            return CreateRequest();
        }
    }
}
