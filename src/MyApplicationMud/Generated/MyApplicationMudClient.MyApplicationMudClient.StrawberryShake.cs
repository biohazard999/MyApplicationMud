﻿// <auto-generated/>
#nullable enable

namespace MyApplicationMud.GraphQL
{
    /// <summary>
    /// Represents the MyApplicationMudClient GraphQL client
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "12.11.1.0")]
    public partial class MyApplicationMudClient : global::MyApplicationMud.GraphQL.IMyApplicationMudClient
    {
        private readonly global::MyApplicationMud.GraphQL.IGetBooksDetailViewQuery _getBooksDetailView;
        private readonly global::MyApplicationMud.GraphQL.IGetBooksListViewQuery _getBooksListView;
        public MyApplicationMudClient(global::MyApplicationMud.GraphQL.IGetBooksDetailViewQuery getBooksDetailView, global::MyApplicationMud.GraphQL.IGetBooksListViewQuery getBooksListView)
        {
            _getBooksDetailView = getBooksDetailView ?? throw new global::System.ArgumentNullException(nameof(getBooksDetailView));
            _getBooksListView = getBooksListView ?? throw new global::System.ArgumentNullException(nameof(getBooksListView));
        }

        public static global::System.String ClientName => "MyApplicationMudClient";
        public global::MyApplicationMud.GraphQL.IGetBooksDetailViewQuery GetBooksDetailView => _getBooksDetailView;
        public global::MyApplicationMud.GraphQL.IGetBooksListViewQuery GetBooksListView => _getBooksListView;
    }
}
