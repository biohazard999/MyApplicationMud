﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class BooksChangedSubscriptionResultFactory : global::StrawberryShake.IOperationResultDataFactory<global::MyApplicationMud.GraphQL.BooksChangedSubscriptionResult>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;
        private readonly global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.BookEntity, BooksChangedSubscription_Changed_Book_Book> _booksChangedSubscription_Changed_Book_BookFromBookEntityMapper;
        private readonly global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.AuthorEntity, GetBooksListView_Items_Author_Author> _getBooksListView_Items_Author_AuthorFromAuthorEntityMapper;
        public BooksChangedSubscriptionResultFactory(global::StrawberryShake.IEntityStore entityStore, global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.BookEntity, BooksChangedSubscription_Changed_Book_Book> booksChangedSubscription_Changed_Book_BookFromBookEntityMapper, global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.AuthorEntity, GetBooksListView_Items_Author_Author> getBooksListView_Items_Author_AuthorFromAuthorEntityMapper)
        {
            _entityStore = entityStore ?? throw new global::System.ArgumentNullException(nameof(entityStore));
            _booksChangedSubscription_Changed_Book_BookFromBookEntityMapper = booksChangedSubscription_Changed_Book_BookFromBookEntityMapper ?? throw new global::System.ArgumentNullException(nameof(booksChangedSubscription_Changed_Book_BookFromBookEntityMapper));
            _getBooksListView_Items_Author_AuthorFromAuthorEntityMapper = getBooksListView_Items_Author_AuthorFromAuthorEntityMapper ?? throw new global::System.ArgumentNullException(nameof(getBooksListView_Items_Author_AuthorFromAuthorEntityMapper));
        }

        global::System.Type global::StrawberryShake.IOperationResultDataFactory.ResultType => typeof(global::MyApplicationMud.GraphQL.IBooksChangedSubscriptionResult);
        public BooksChangedSubscriptionResult Create(global::StrawberryShake.IOperationResultDataInfo dataInfo, global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            if (dataInfo is BooksChangedSubscriptionResultInfo info)
            {
                return new BooksChangedSubscriptionResult(MapNonNullableIBooksChangedSubscription_Changed(info.Changed, snapshot));
            }

            throw new global::System.ArgumentException("BooksChangedSubscriptionResultInfo expected.");
        }

        private global::MyApplicationMud.GraphQL.IBooksChangedSubscription_Changed MapNonNullableIBooksChangedSubscription_Changed(global::MyApplicationMud.GraphQL.State.BookChangedPayloadData data, global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            IBooksChangedSubscription_Changed returnValue = default !;
            if (data.__typename.Equals("BookChangedPayload", global::System.StringComparison.Ordinal))
            {
                returnValue = new BooksChangedSubscription_Changed_BookChangedPayload(data.Type ?? throw new global::System.ArgumentNullException(), MapNonNullableIBooksChangedSubscription_Changed_Book(data.Book ?? throw new global::System.ArgumentNullException(), snapshot));
            }
            else
            {
                throw new global::System.NotSupportedException();
            }

            return returnValue;
        }

        private global::MyApplicationMud.GraphQL.IBooksChangedSubscription_Changed_Book MapNonNullableIBooksChangedSubscription_Changed_Book(global::StrawberryShake.EntityId entityId, global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            if (entityId.Name.Equals("Book", global::System.StringComparison.Ordinal))
            {
                return _booksChangedSubscription_Changed_Book_BookFromBookEntityMapper.Map(snapshot.GetEntity<global::MyApplicationMud.GraphQL.State.BookEntity>(entityId) ?? throw new global::StrawberryShake.GraphQLClientException());
            }

            throw new global::System.NotSupportedException();
        }

        private global::MyApplicationMud.GraphQL.IGetBooksListView_Items_Author MapNonNullableIGetBooksListView_Items_Author(global::StrawberryShake.EntityId entityId, global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            if (entityId.Name.Equals("Author", global::System.StringComparison.Ordinal))
            {
                return _getBooksListView_Items_Author_AuthorFromAuthorEntityMapper.Map(snapshot.GetEntity<global::MyApplicationMud.GraphQL.State.AuthorEntity>(entityId) ?? throw new global::StrawberryShake.GraphQLClientException());
            }

            throw new global::System.NotSupportedException();
        }

        global::System.Object global::StrawberryShake.IOperationResultDataFactory.Create(global::StrawberryShake.IOperationResultDataInfo dataInfo, global::StrawberryShake.IEntityStoreSnapshot? snapshot)
        {
            return Create(dataInfo, snapshot);
        }
    }
}
