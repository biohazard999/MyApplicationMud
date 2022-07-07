﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class ComparableInt32OperationFilterInputInputValueFormatter : global::StrawberryShake.Serialization.IInputObjectFormatter
    {
        private global::StrawberryShake.Serialization.IInputValueFormatter _intFormatter = default !;
        public global::System.String TypeName => "ComparableInt32OperationFilterInput";
        public void Initialize(global::StrawberryShake.Serialization.ISerializerResolver serializerResolver)
        {
            _intFormatter = serializerResolver.GetInputValueFormatter("Int");
        }

        public global::System.Object? Format(global::System.Object? runtimeValue)
        {
            if (runtimeValue is null)
            {
                return null;
            }

            var input = runtimeValue as global::MyApplicationMud.GraphQL.ComparableInt32OperationFilterInput;
            var inputInfo = runtimeValue as global::MyApplicationMud.GraphQL.State.IComparableInt32OperationFilterInputInfo;
            if (input is null || inputInfo is null)
            {
                throw new global::System.ArgumentException(nameof(runtimeValue));
            }

            var fields = new global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>>();
            if (inputInfo.IsEqSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("eq", FormatEq(input.Eq)));
            }

            if (inputInfo.IsNeqSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("neq", FormatNeq(input.Neq)));
            }

            if (inputInfo.IsInSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("in", FormatIn(input.In)));
            }

            if (inputInfo.IsNinSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("nin", FormatNin(input.Nin)));
            }

            if (inputInfo.IsGtSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("gt", FormatGt(input.Gt)));
            }

            if (inputInfo.IsNgtSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("ngt", FormatNgt(input.Ngt)));
            }

            if (inputInfo.IsGteSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("gte", FormatGte(input.Gte)));
            }

            if (inputInfo.IsNgteSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("ngte", FormatNgte(input.Ngte)));
            }

            if (inputInfo.IsLtSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("lt", FormatLt(input.Lt)));
            }

            if (inputInfo.IsNltSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("nlt", FormatNlt(input.Nlt)));
            }

            if (inputInfo.IsLteSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("lte", FormatLte(input.Lte)));
            }

            if (inputInfo.IsNlteSet)
            {
                fields.Add(new global::System.Collections.Generic.KeyValuePair<global::System.String, global::System.Object?>("nlte", FormatNlte(input.Nlte)));
            }

            return fields;
        }

        private global::System.Object? FormatEq(global::System.Int32? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                return _intFormatter.Format(input);
            }
        }

        private global::System.Object? FormatNeq(global::System.Int32? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                return _intFormatter.Format(input);
            }
        }

        private global::System.Object? FormatIn(global::System.Collections.Generic.IReadOnlyList<global::System.Int32>? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                var input_list = new global::System.Collections.Generic.List<global::System.Object?>();
                foreach (var input_elm in input)
                {
                    input_list.Add(_intFormatter.Format(input_elm));
                }

                return input_list;
            }
        }

        private global::System.Object? FormatNin(global::System.Collections.Generic.IReadOnlyList<global::System.Int32>? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                var input_list = new global::System.Collections.Generic.List<global::System.Object?>();
                foreach (var input_elm in input)
                {
                    input_list.Add(_intFormatter.Format(input_elm));
                }

                return input_list;
            }
        }

        private global::System.Object? FormatGt(global::System.Int32? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                return _intFormatter.Format(input);
            }
        }

        private global::System.Object? FormatNgt(global::System.Int32? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                return _intFormatter.Format(input);
            }
        }

        private global::System.Object? FormatGte(global::System.Int32? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                return _intFormatter.Format(input);
            }
        }

        private global::System.Object? FormatNgte(global::System.Int32? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                return _intFormatter.Format(input);
            }
        }

        private global::System.Object? FormatLt(global::System.Int32? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                return _intFormatter.Format(input);
            }
        }

        private global::System.Object? FormatNlt(global::System.Int32? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                return _intFormatter.Format(input);
            }
        }

        private global::System.Object? FormatLte(global::System.Int32? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                return _intFormatter.Format(input);
            }
        }

        private global::System.Object? FormatNlte(global::System.Int32? input)
        {
            if (input is null)
            {
                return input;
            }
            else
            {
                return _intFormatter.Format(input);
            }
        }
    }
}