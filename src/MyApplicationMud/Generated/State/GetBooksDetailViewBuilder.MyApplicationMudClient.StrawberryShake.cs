﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class GetBooksDetailViewBuilder : global::StrawberryShake.IOperationResultBuilder<global::System.Text.Json.JsonDocument, global::MyApplicationMud.GraphQL.IGetBooksDetailViewResult>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;
        private readonly global::StrawberryShake.IEntityIdSerializer _idSerializer;
        private readonly global::StrawberryShake.IOperationResultDataFactory<global::MyApplicationMud.GraphQL.IGetBooksDetailViewResult> _resultDataFactory;
        private readonly global::StrawberryShake.Serialization.ILeafValueParser<global::System.Int32, global::System.Int32> _intParser;
        private readonly global::StrawberryShake.Serialization.ILeafValueParser<global::System.String, global::System.String> _stringParser;
        public GetBooksDetailViewBuilder(global::StrawberryShake.IEntityStore entityStore, global::StrawberryShake.IEntityIdSerializer idSerializer, global::StrawberryShake.IOperationResultDataFactory<global::MyApplicationMud.GraphQL.IGetBooksDetailViewResult> resultDataFactory, global::StrawberryShake.Serialization.ISerializerResolver serializerResolver)
        {
            _entityStore = entityStore ?? throw new global::System.ArgumentNullException(nameof(entityStore));
            _idSerializer = idSerializer ?? throw new global::System.ArgumentNullException(nameof(idSerializer));
            _resultDataFactory = resultDataFactory ?? throw new global::System.ArgumentNullException(nameof(resultDataFactory));
            _intParser = serializerResolver.GetLeafValueParser<global::System.Int32, global::System.Int32>("Int") ?? throw new global::System.ArgumentException("No serializer for type `Int` found.");
            _stringParser = serializerResolver.GetLeafValueParser<global::System.String, global::System.String>("String") ?? throw new global::System.ArgumentException("No serializer for type `String` found.");
        }

        public global::StrawberryShake.IOperationResult<IGetBooksDetailViewResult> Build(global::StrawberryShake.Response<global::System.Text.Json.JsonDocument> response)
        {
            (IGetBooksDetailViewResult Result, GetBooksDetailViewResultInfo Info)? data = null;
            global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.IClientError>? errors = null;
            if (response.Exception is null)
            {
                try
                {
                    if (response.Body != null)
                    {
                        if (response.Body.RootElement.TryGetProperty("data", out global::System.Text.Json.JsonElement dataElement) && dataElement.ValueKind == global::System.Text.Json.JsonValueKind.Object)
                        {
                            data = BuildData(dataElement);
                        }

                        if (response.Body.RootElement.TryGetProperty("errors", out global::System.Text.Json.JsonElement errorsElement))
                        {
                            errors = global::StrawberryShake.Json.JsonErrorParser.ParseErrors(errorsElement);
                        }
                    }
                }
                catch (global::System.Exception ex)
                {
                    errors = new global::StrawberryShake.IClientError[]{new global::StrawberryShake.ClientError(ex.Message, exception: ex, extensions: new global::System.Collections.Generic.Dictionary<global::System.String, global::System.Object?>{{"body", response.Body?.RootElement.ToString()}})};
                }
            }
            else
            {
                if (response.Body != null && response.Body.RootElement.TryGetProperty("errors", out global::System.Text.Json.JsonElement errorsElement))
                {
                    errors = global::StrawberryShake.Json.JsonErrorParser.ParseErrors(errorsElement);
                }
                else
                {
                    errors = new global::StrawberryShake.IClientError[]{new global::StrawberryShake.ClientError(response.Exception.Message, exception: response.Exception, extensions: new global::System.Collections.Generic.Dictionary<global::System.String, global::System.Object?>{{"body", response.Body?.RootElement.ToString()}})};
                }
            }

            return new global::StrawberryShake.OperationResult<IGetBooksDetailViewResult>(data?.Result, data?.Info, _resultDataFactory, errors);
        }

        private (IGetBooksDetailViewResult, GetBooksDetailViewResultInfo) BuildData(global::System.Text.Json.JsonElement obj)
        {
            var entityIds = new global::System.Collections.Generic.HashSet<global::StrawberryShake.EntityId>();
            global::StrawberryShake.IEntityStoreSnapshot snapshot = default !;
            global::StrawberryShake.EntityId detailsId = default !;
            _entityStore.Update(session =>
            {
                detailsId = UpdateNonNullableIGetBooksDetailView_DetailsEntity(session, global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(obj, "details"), entityIds);
                snapshot = session.CurrentSnapshot;
            });
            var resultInfo = new GetBooksDetailViewResultInfo(detailsId, entityIds, snapshot.Version);
            return (_resultDataFactory.Create(resultInfo), resultInfo);
        }

        private global::StrawberryShake.EntityId UpdateNonNullableIGetBooksDetailView_DetailsEntity(global::StrawberryShake.IEntityStoreUpdateSession session, global::System.Text.Json.JsonElement? obj, global::System.Collections.Generic.ISet<global::StrawberryShake.EntityId> entityIds)
        {
            if (!obj.HasValue)
            {
                throw new global::System.ArgumentNullException();
            }

            global::StrawberryShake.EntityId entityId = _idSerializer.Parse(obj.Value);
            entityIds.Add(entityId);
            if (entityId.Name.Equals("Book", global::System.StringComparison.Ordinal))
            {
                if (session.CurrentSnapshot.TryGetEntity(entityId, out global::MyApplicationMud.GraphQL.State.BookEntity? entity))
                {
                    session.SetEntity(entityId, new global::MyApplicationMud.GraphQL.State.BookEntity(DeserializeNonNullableInt32(global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(obj, "id")), DeserializeNonNullableString(global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(obj, "title")), entity.Image, entity.Author));
                }
                else
                {
                    session.SetEntity(entityId, new global::MyApplicationMud.GraphQL.State.BookEntity(DeserializeNonNullableInt32(global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(obj, "id")), DeserializeNonNullableString(global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(obj, "title")), default !, default !));
                }

                return entityId;
            }

            throw new global::System.NotSupportedException();
        }

        private global::System.Int32 DeserializeNonNullableInt32(global::System.Text.Json.JsonElement? obj)
        {
            if (!obj.HasValue)
            {
                throw new global::System.ArgumentNullException();
            }

            return _intParser.Parse(obj.Value.GetInt32()!);
        }

        private global::System.String DeserializeNonNullableString(global::System.Text.Json.JsonElement? obj)
        {
            if (!obj.HasValue)
            {
                throw new global::System.ArgumentNullException();
            }

            return _stringParser.Parse(obj.Value.GetString()!);
        }
    }
}
