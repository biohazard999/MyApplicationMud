﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class UpdateBookResultFactory : global::StrawberryShake.IOperationResultDataFactory<global::MyApplicationMud.GraphQL.UpdateBookResult>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;
        private readonly global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.BookEntity, UpdateBook_EditBook_Value_Book> _updateBook_EditBook_Value_BookFromBookEntityMapper;
        private readonly global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.AuthorEntity, GetBooksDetailView_Details_Author_Author> _getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper;
        public UpdateBookResultFactory(global::StrawberryShake.IEntityStore entityStore, global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.BookEntity, UpdateBook_EditBook_Value_Book> updateBook_EditBook_Value_BookFromBookEntityMapper, global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.AuthorEntity, GetBooksDetailView_Details_Author_Author> getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper)
        {
            _entityStore = entityStore ?? throw new global::System.ArgumentNullException(nameof(entityStore));
            _updateBook_EditBook_Value_BookFromBookEntityMapper = updateBook_EditBook_Value_BookFromBookEntityMapper ?? throw new global::System.ArgumentNullException(nameof(updateBook_EditBook_Value_BookFromBookEntityMapper));
            _getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper = getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper ?? throw new global::System.ArgumentNullException(nameof(getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper));
        }

        global::System.Type global::StrawberryShake.IOperationResultDataFactory.ResultType => typeof(global::MyApplicationMud.GraphQL.IUpdateBookResult);
        public UpdateBookResult Create(global::StrawberryShake.IOperationResultDataInfo dataInfo, global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            if (dataInfo is UpdateBookResultInfo info)
            {
                return new UpdateBookResult(MapNonNullableIUpdateBook_EditBook(info.EditBook, snapshot));
            }

            throw new global::System.ArgumentException("UpdateBookResultInfo expected.");
        }

        private global::MyApplicationMud.GraphQL.IUpdateBook_EditBook MapNonNullableIUpdateBook_EditBook(global::MyApplicationMud.GraphQL.State.BookValidationPayloadData data, global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            IUpdateBook_EditBook returnValue = default !;
            if (data.__typename.Equals("BookValidationPayload", global::System.StringComparison.Ordinal))
            {
                returnValue = new UpdateBook_EditBook_BookValidationPayload(MapNonNullableIUpdateBook_EditBook_ErrorsNonNullableArray(data.Errors ?? throw new global::System.ArgumentNullException(), snapshot), MapIUpdateBook_EditBook_Value(data.Value, snapshot));
            }
            else
            {
                throw new global::System.NotSupportedException();
            }

            return returnValue;
        }

        private global::System.Collections.Generic.IReadOnlyList<global::MyApplicationMud.GraphQL.IUpdateBook_EditBook_Errors> MapNonNullableIUpdateBook_EditBook_ErrorsNonNullableArray(global::System.Collections.Generic.IReadOnlyList<global::MyApplicationMud.GraphQL.State.ValidationErrorData>? list, global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            if (list is null)
            {
                throw new global::System.ArgumentNullException();
            }

            var validationErrors = new global::System.Collections.Generic.List<global::MyApplicationMud.GraphQL.IUpdateBook_EditBook_Errors>();
            foreach (global::MyApplicationMud.GraphQL.State.ValidationErrorData child in list)
            {
                validationErrors.Add(MapNonNullableIUpdateBook_EditBook_Errors(child, snapshot));
            }

            return validationErrors;
        }

        private global::MyApplicationMud.GraphQL.IUpdateBook_EditBook_Errors MapNonNullableIUpdateBook_EditBook_Errors(global::MyApplicationMud.GraphQL.State.ValidationErrorData data, global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            IUpdateBook_EditBook_Errors returnValue = default !;
            if (data.__typename.Equals("ValidationError", global::System.StringComparison.Ordinal))
            {
                returnValue = new UpdateBook_EditBook_Errors_ValidationError(data.PropertyName ?? throw new global::System.ArgumentNullException(), data.Message ?? throw new global::System.ArgumentNullException());
            }
            else
            {
                throw new global::System.NotSupportedException();
            }

            return returnValue;
        }

        private global::MyApplicationMud.GraphQL.IUpdateBook_EditBook_Value? MapIUpdateBook_EditBook_Value(global::StrawberryShake.EntityId? entityId, global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            if (entityId is null)
            {
                return null;
            }

            if (entityId.Value.Name.Equals("Book", global::System.StringComparison.Ordinal))
            {
                return _updateBook_EditBook_Value_BookFromBookEntityMapper.Map(snapshot.GetEntity<global::MyApplicationMud.GraphQL.State.BookEntity>(entityId.Value) ?? throw new global::StrawberryShake.GraphQLClientException());
            }

            throw new global::System.NotSupportedException();
        }

        private global::MyApplicationMud.GraphQL.IGetBooksDetailView_Details_Author MapNonNullableIGetBooksDetailView_Details_Author(global::StrawberryShake.EntityId entityId, global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            if (entityId.Name.Equals("Author", global::System.StringComparison.Ordinal))
            {
                return _getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper.Map(snapshot.GetEntity<global::MyApplicationMud.GraphQL.State.AuthorEntity>(entityId) ?? throw new global::StrawberryShake.GraphQLClientException());
            }

            throw new global::System.NotSupportedException();
        }

        global::System.Object global::StrawberryShake.IOperationResultDataFactory.Create(global::StrawberryShake.IOperationResultDataInfo dataInfo, global::StrawberryShake.IEntityStoreSnapshot? snapshot)
        {
            return Create(dataInfo, snapshot);
        }
    }
}
