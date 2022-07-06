﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class StringOperationFilterInput : global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo, global::System.IEquatable<StringOperationFilterInput>
    {
        public override global::System.Boolean Equals(global::System.Object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((StringOperationFilterInput)obj);
        }

        public virtual global::System.Boolean Equals(StringOperationFilterInput? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return (global::StrawberryShake.Helper.ComparisonHelper.SequenceEqual(And, other.And)) && global::StrawberryShake.Helper.ComparisonHelper.SequenceEqual(Or, other.Or) && ((Eq is null && other.Eq is null) || Eq != null && Eq.Equals(other.Eq)) && ((Neq is null && other.Neq is null) || Neq != null && Neq.Equals(other.Neq)) && ((Contains is null && other.Contains is null) || Contains != null && Contains.Equals(other.Contains)) && ((Ncontains is null && other.Ncontains is null) || Ncontains != null && Ncontains.Equals(other.Ncontains)) && global::StrawberryShake.Helper.ComparisonHelper.SequenceEqual(In, other.In) && global::StrawberryShake.Helper.ComparisonHelper.SequenceEqual(Nin, other.Nin) && ((StartsWith is null && other.StartsWith is null) || StartsWith != null && StartsWith.Equals(other.StartsWith)) && ((NstartsWith is null && other.NstartsWith is null) || NstartsWith != null && NstartsWith.Equals(other.NstartsWith)) && ((EndsWith is null && other.EndsWith is null) || EndsWith != null && EndsWith.Equals(other.EndsWith)) && ((NendsWith is null && other.NendsWith is null) || NendsWith != null && NendsWith.Equals(other.NendsWith));
        }

        public override global::System.Int32 GetHashCode()
        {
            unchecked
            {
                int hash = 5;
                if (And != null)
                {
                    foreach (var And_elm in And)
                    {
                        hash ^= 397 * And_elm.GetHashCode();
                    }
                }

                if (Or != null)
                {
                    foreach (var Or_elm in Or)
                    {
                        hash ^= 397 * Or_elm.GetHashCode();
                    }
                }

                if (Eq != null)
                {
                    hash ^= 397 * Eq.GetHashCode();
                }

                if (Neq != null)
                {
                    hash ^= 397 * Neq.GetHashCode();
                }

                if (Contains != null)
                {
                    hash ^= 397 * Contains.GetHashCode();
                }

                if (Ncontains != null)
                {
                    hash ^= 397 * Ncontains.GetHashCode();
                }

                if (In != null)
                {
                    foreach (var In_elm in In)
                    {
                        if (In_elm != null)
                        {
                            hash ^= 397 * In_elm.GetHashCode();
                        }
                    }
                }

                if (Nin != null)
                {
                    foreach (var Nin_elm in Nin)
                    {
                        if (Nin_elm != null)
                        {
                            hash ^= 397 * Nin_elm.GetHashCode();
                        }
                    }
                }

                if (StartsWith != null)
                {
                    hash ^= 397 * StartsWith.GetHashCode();
                }

                if (NstartsWith != null)
                {
                    hash ^= 397 * NstartsWith.GetHashCode();
                }

                if (EndsWith != null)
                {
                    hash ^= 397 * EndsWith.GetHashCode();
                }

                if (NendsWith != null)
                {
                    hash ^= 397 * NendsWith.GetHashCode();
                }

                return hash;
            }
        }

        private global::System.Collections.Generic.IReadOnlyList<global::MyApplicationMud.GraphQL.StringOperationFilterInput>? _value_and;
        private global::System.Boolean _set_and;
        private global::System.Collections.Generic.IReadOnlyList<global::MyApplicationMud.GraphQL.StringOperationFilterInput>? _value_or;
        private global::System.Boolean _set_or;
        private global::System.String? _value_eq;
        private global::System.Boolean _set_eq;
        private global::System.String? _value_neq;
        private global::System.Boolean _set_neq;
        private global::System.String? _value_contains;
        private global::System.Boolean _set_contains;
        private global::System.String? _value_ncontains;
        private global::System.Boolean _set_ncontains;
        private global::System.Collections.Generic.IReadOnlyList<global::System.String?>? _value_in;
        private global::System.Boolean _set_in;
        private global::System.Collections.Generic.IReadOnlyList<global::System.String?>? _value_nin;
        private global::System.Boolean _set_nin;
        private global::System.String? _value_startsWith;
        private global::System.Boolean _set_startsWith;
        private global::System.String? _value_nstartsWith;
        private global::System.Boolean _set_nstartsWith;
        private global::System.String? _value_endsWith;
        private global::System.Boolean _set_endsWith;
        private global::System.String? _value_nendsWith;
        private global::System.Boolean _set_nendsWith;
        public global::System.Collections.Generic.IReadOnlyList<global::MyApplicationMud.GraphQL.StringOperationFilterInput>? And
        {
            get => _value_and;
            set
            {
                _set_and = true;
                _value_and = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsAndSet => _set_and;
        public global::System.Collections.Generic.IReadOnlyList<global::MyApplicationMud.GraphQL.StringOperationFilterInput>? Or
        {
            get => _value_or;
            set
            {
                _set_or = true;
                _value_or = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsOrSet => _set_or;
        public global::System.String? Eq
        {
            get => _value_eq;
            set
            {
                _set_eq = true;
                _value_eq = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsEqSet => _set_eq;
        public global::System.String? Neq
        {
            get => _value_neq;
            set
            {
                _set_neq = true;
                _value_neq = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsNeqSet => _set_neq;
        public global::System.String? Contains
        {
            get => _value_contains;
            set
            {
                _set_contains = true;
                _value_contains = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsContainsSet => _set_contains;
        public global::System.String? Ncontains
        {
            get => _value_ncontains;
            set
            {
                _set_ncontains = true;
                _value_ncontains = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsNcontainsSet => _set_ncontains;
        public global::System.Collections.Generic.IReadOnlyList<global::System.String?>? In
        {
            get => _value_in;
            set
            {
                _set_in = true;
                _value_in = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsInSet => _set_in;
        public global::System.Collections.Generic.IReadOnlyList<global::System.String?>? Nin
        {
            get => _value_nin;
            set
            {
                _set_nin = true;
                _value_nin = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsNinSet => _set_nin;
        public global::System.String? StartsWith
        {
            get => _value_startsWith;
            set
            {
                _set_startsWith = true;
                _value_startsWith = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsStartsWithSet => _set_startsWith;
        public global::System.String? NstartsWith
        {
            get => _value_nstartsWith;
            set
            {
                _set_nstartsWith = true;
                _value_nstartsWith = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsNstartsWithSet => _set_nstartsWith;
        public global::System.String? EndsWith
        {
            get => _value_endsWith;
            set
            {
                _set_endsWith = true;
                _value_endsWith = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsEndsWithSet => _set_endsWith;
        public global::System.String? NendsWith
        {
            get => _value_nendsWith;
            set
            {
                _set_nendsWith = true;
                _value_nendsWith = value;
            }
        }

        global::System.Boolean global::MyApplicationMud.GraphQL.State.IStringOperationFilterInputInfo.IsNendsWithSet => _set_nendsWith;
    }
}