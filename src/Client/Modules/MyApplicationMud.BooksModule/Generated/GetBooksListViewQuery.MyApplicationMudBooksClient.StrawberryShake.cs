﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    /// <summary>
    /// Represents the operation service of the GetBooksListView GraphQL operation
    /// <code>
    /// query GetBooksListView($where: BookFilterInput!) {
    ///   items: books(where: $where) {
    ///     __typename
    ///     ... BookListInfo
    ///     ... on Book {
    ///       id
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
    public partial class GetBooksListViewQuery : global::MyApplicationMud.GraphQL.IGetBooksListViewQuery
    {
        private readonly global::StrawberryShake.IOperationExecutor<IGetBooksListViewResult> _operationExecutor;
        private readonly global::StrawberryShake.Serialization.IInputValueFormatter _bookFilterInputFormatter;
        public GetBooksListViewQuery(global::StrawberryShake.IOperationExecutor<IGetBooksListViewResult> operationExecutor, global::StrawberryShake.Serialization.ISerializerResolver serializerResolver)
        {
            _operationExecutor = operationExecutor ?? throw new global::System.ArgumentNullException(nameof(operationExecutor));
            _bookFilterInputFormatter = serializerResolver.GetInputValueFormatter("BookFilterInput");
        }

        global::System.Type global::StrawberryShake.IOperationRequestFactory.ResultType => typeof(IGetBooksListViewResult);
        public async global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<IGetBooksListViewResult>> ExecuteAsync(global::MyApplicationMud.GraphQL.BookFilterInput @where, global::System.Threading.CancellationToken cancellationToken = default)
        {
            var request = CreateRequest(@where);
            return await _operationExecutor.ExecuteAsync(request, cancellationToken).ConfigureAwait(false);
        }

        public global::System.IObservable<global::StrawberryShake.IOperationResult<IGetBooksListViewResult>> Watch(global::MyApplicationMud.GraphQL.BookFilterInput @where, global::StrawberryShake.ExecutionStrategy? strategy = null)
        {
            var request = CreateRequest(@where);
            return _operationExecutor.Watch(request, strategy);
        }

        private global::StrawberryShake.OperationRequest CreateRequest(global::MyApplicationMud.GraphQL.BookFilterInput @where)
        {
            var variables = new global::System.Collections.Generic.Dictionary<global::System.String, global::System.Object?>();
            variables.Add("where", FormatWhere(@where));
            return CreateRequest(variables);
        }

        private global::StrawberryShake.OperationRequest CreateRequest(global::System.Collections.Generic.IReadOnlyDictionary<global::System.String, global::System.Object?>? variables)
        {
            return new global::StrawberryShake.OperationRequest(id: GetBooksListViewQueryDocument.Instance.Hash.Value, name: "GetBooksListView", document: GetBooksListViewQueryDocument.Instance, strategy: global::StrawberryShake.RequestStrategy.Default, variables: variables);
        }

        private global::System.Object? FormatWhere(global::MyApplicationMud.GraphQL.BookFilterInput value)
        {
            if (value is null)
            {
                throw new global::System.ArgumentNullException(nameof(value));
            }

            return _bookFilterInputFormatter.Format(value);
        }

        global::StrawberryShake.OperationRequest global::StrawberryShake.IOperationRequestFactory.Create(global::System.Collections.Generic.IReadOnlyDictionary<global::System.String, global::System.Object?>? variables)
        {
            return CreateRequest(variables!);
        }
    }
}