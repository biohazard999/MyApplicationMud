﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial record BookChangedPayloadData
    {
        public BookChangedPayloadData(global::System.String __typename, global::MyApplicationMud.GraphQL.ChangeType? type = default !, global::StrawberryShake.EntityId? book = default !)
        {
            this.__typename = __typename ?? throw new global::System.ArgumentNullException(nameof(__typename));
            Type = type;
            Book = book;
        }

        public global::System.String __typename { get; init; }

        public global::MyApplicationMud.GraphQL.ChangeType? Type { get; init; }

        public global::StrawberryShake.EntityId? Book { get; init; }
    }
}
