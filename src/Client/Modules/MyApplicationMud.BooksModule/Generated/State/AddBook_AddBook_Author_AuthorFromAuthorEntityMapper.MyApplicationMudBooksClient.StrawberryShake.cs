﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class AddBook_AddBook_Author_AuthorFromAuthorEntityMapper : global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.AuthorEntity, AddBook_AddBook_Author_Author>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;
        public AddBook_AddBook_Author_AuthorFromAuthorEntityMapper(global::StrawberryShake.IEntityStore entityStore)
        {
            _entityStore = entityStore ?? throw new global::System.ArgumentNullException(nameof(entityStore));
        }

        public AddBook_AddBook_Author_Author Map(global::MyApplicationMud.GraphQL.State.AuthorEntity entity, global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            return new AddBook_AddBook_Author_Author(entity.Id, entity.Name);
        }
    }
}
