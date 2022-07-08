using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.Routing;

namespace MyApplicationMud;

public record BooksModule() : IModule
{
    public string Name => "App";

    public List<NavItem> NavItems => new List<NavItem>
    {
        new("books", "Books", Icons.Material.Filled.Book, NavLinkMatch.All)
    };
}
