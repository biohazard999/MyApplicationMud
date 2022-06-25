﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class BookModelInputInputValueFormatter : global::StrawberryShake.Serialization.IInputObjectFormatter
    {
        private global::StrawberryShake.Serialization.IInputValueFormatter _stringFormatter = default !;
        private global::StrawberryShake.Serialization.IInputValueFormatter _intFormatter = default !;
        public global::System.String TypeName => "BookModelInput";
        public void Initialize(global::StrawberryShake.Serialization.ISerializerResolver serializerResolver)
        {
            _stringFormatter = serializerResolver.GetInputValueFormatter("String");
            _intFormatter = serializerResolver.GetInputValueFormatter("Int");
        }

        public global::System.Object? Format(global::System.Object? runtimeValue)
        {
            if (runtimeValue is null)
            {
                return null;
            }

            var input = runtimeValue as global::MyApplicationMud.GraphQL.BookModelInput;
            var inputInfo = runtimeValue as global::MyApplicationMud.GraphQL.State.IBookModelInputInfo;
            if (input is null || inputInfo is null)
            {
                throw new global::System.ArgumentException(nameof(runtimeValue));
            }

            var fields = new global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>>();
            if (inputInfo.IsTitleSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("title", FormatTitle(input.Title)));
            }

            if (inputInfo.IsAuthorIdSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("authorId", FormatAuthorId(input.AuthorId)));
            }

            return fields;
        }

        private global::System.Object? FormatTitle(global::System.String input)
        {
            if (input is null)
            {
                throw new global::System.ArgumentNullException(nameof(input));
            }

            return _stringFormatter.Format(input);
        }

        private global::System.Object? FormatAuthorId(global::System.Int32 input)
        {
            return _intFormatter.Format(input);
        }
    }
}