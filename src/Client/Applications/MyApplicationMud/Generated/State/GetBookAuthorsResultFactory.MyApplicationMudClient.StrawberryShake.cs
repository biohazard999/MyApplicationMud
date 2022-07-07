﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class GetBookAuthorsResultFactory : global::StrawberryShake.IOperationResultDataFactory<global::MyApplicationMud.GraphQL.GetBookAuthorsResult>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;
        private readonly global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.AuthorEntity, GetBookAuthors_Authors_Author> _getBookAuthors_Authors_AuthorFromAuthorEntityMapper;
        public GetBookAuthorsResultFactory(global::StrawberryShake.IEntityStore entityStore, global::StrawberryShake.IEntityMapper<global::MyApplicationMud.GraphQL.State.AuthorEntity, GetBookAuthors_Authors_Author> getBookAuthors_Authors_AuthorFromAuthorEntityMapper)
        {
            _entityStore = entityStore ?? throw new global::System.ArgumentNullException(nameof(entityStore));
            _getBookAuthors_Authors_AuthorFromAuthorEntityMapper = getBookAuthors_Authors_AuthorFromAuthorEntityMapper ?? throw new global::System.ArgumentNullException(nameof(getBookAuthors_Authors_AuthorFromAuthorEntityMapper));
        }

        global::System.Type global::StrawberryShake.IOperationResultDataFactory.ResultType => typeof(global::MyApplicationMud.GraphQL.IGetBookAuthorsResult);
        public GetBookAuthorsResult Create(global::StrawberryShake.IOperationResultDataInfo dataInfo, global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            if (dataInfo is GetBookAuthorsResultInfo info)
            {
                return new GetBookAuthorsResult(MapNonNullableIGetBookAuthors_AuthorsNonNullableArray(info.Authors, snapshot));
            }

            throw new global::System.ArgumentException("GetBookAuthorsResultInfo expected.");
        }

        private global::System.Collections.Generic.IReadOnlyList<global::MyApplicationMud.GraphQL.IGetBookAuthors_Authors> MapNonNullableIGetBookAuthors_AuthorsNonNullableArray(global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.EntityId>? list, global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            if (list is null)
            {
                throw new global::System.ArgumentNullException();
            }

            var authors = new global::System.Collections.Generic.List<global::MyApplicationMud.GraphQL.IGetBookAuthors_Authors>();
            foreach (global::StrawberryShake.EntityId child in list)
            {
                authors.Add(MapNonNullableIGetBookAuthors_Authors(child, snapshot));
            }

            return authors;
        }

        private global::MyApplicationMud.GraphQL.IGetBookAuthors_Authors MapNonNullableIGetBookAuthors_Authors(global::StrawberryShake.EntityId entityId, global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            if (entityId.Name.Equals("Author", global::System.StringComparison.Ordinal))
            {
                return _getBookAuthors_Authors_AuthorFromAuthorEntityMapper.Map(snapshot.GetEntity<global::MyApplicationMud.GraphQL.State.AuthorEntity>(entityId) ?? throw new global::StrawberryShake.GraphQLClientException());
            }

            throw new global::System.NotSupportedException();
        }

        global::System.Object global::StrawberryShake.IOperationResultDataFactory.Create(global::StrawberryShake.IOperationResultDataInfo dataInfo, global::StrawberryShake.IEntityStoreSnapshot? snapshot)
        {
            return Create(dataInfo, snapshot);
        }
    }
}