﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class UpdateBook_EditBook_BookFromBookEntityMapper : global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.BookEntity, UpdateBook_EditBook_Book>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;
        private readonly global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.AuthorEntity, GetBooksDetailView_Details_Author_Author> _getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper;
        public UpdateBook_EditBook_BookFromBookEntityMapper(global::StrawberryShake.IEntityStore entityStore, global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.AuthorEntity, GetBooksDetailView_Details_Author_Author> getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper)
        {
            _entityStore = entityStore ?? throw new global::System.ArgumentNullException(nameof(entityStore));
            _getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper = getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper ?? throw new global::System.ArgumentNullException(nameof(getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper));
        }

        public UpdateBook_EditBook_Book Map(global::MyApplicationMud.GraphQL.State.BookEntity entity, global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            return new UpdateBook_EditBook_Book(entity.Id, entity.Title, entity.Image, MapNonNullableIGetBooksDetailView_Details_Author(entity.Author, snapshot));
        }

        private global::MyApplicationMud.GraphQL.IGetBooksDetailView_Details_Author MapNonNullableIGetBooksDetailView_Details_Author(global::StrawberryShake.EntityId entityId, global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            if (entityId.Name.Equals("Author", global::System.StringComparison.Ordinal))
            {
                return _getBooksDetailView_Details_Author_AuthorFromAuthorEntityMapper.Map(snapshot.GetEntity<global::MyApplicationMud.GraphQL.State.AuthorEntity>(entityId) ?? throw new global::StrawberryShake.GraphQLClientException());
            }

            throw new global::System.NotSupportedException();
        }
    }
}
