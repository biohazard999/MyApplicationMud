﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class GetBooksDetailViewResultInfo : global::StrawberryShake.IOperationResultDataInfo
    {
        private readonly global::System.Collections.Generic.IReadOnlyCollection<global::StrawberryShake.EntityId> _entityIds;
        private readonly global::System.UInt64 _version;
        public GetBooksDetailViewResultInfo(global::StrawberryShake.EntityId details, global::System.Collections.Generic.IReadOnlyCollection<global::StrawberryShake.EntityId> entityIds, global::System.UInt64 version)
        {
            Details = details;
            _entityIds = entityIds ?? throw new global::System.ArgumentNullException(nameof(entityIds));
            _version = version;
        }

        public global::StrawberryShake.EntityId Details { get; }

        public global::System.Collections.Generic.IReadOnlyCollection<global::StrawberryShake.EntityId> EntityIds => _entityIds;
        public global::System.UInt64 Version => _version;
        public global::StrawberryShake.IOperationResultDataInfo WithVersion(global::System.UInt64 version)
        {
            return new GetBooksDetailViewResultInfo(Details, _entityIds, version);
        }
    }
}
