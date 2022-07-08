using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.Routing;

namespace MyApplicationMud;

public record AppModule() : IModule
{
    public string Name => "App";

    public List<NavItem> NavItems => new List<NavItem>
    {
        new("", "Home", Icons.Material.Filled.Home, NavLinkMatch.All)
    };
}
