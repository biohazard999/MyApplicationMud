﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial record ValidationErrorData
    {
        public ValidationErrorData(global::System.String __typename, global::System.String? propertyName = default !, global::System.String? message = default !)
        {
            this.__typename = __typename ?? throw new global::System.ArgumentNullException(nameof(__typename));
            PropertyName = propertyName;
            Message = message;
        }

        public global::System.String __typename { get; init; }

        public global::System.String? PropertyName { get; init; }

        public global::System.String? Message { get; init; }
    }
}
