using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.Routing;

namespace MyApplicationMud;

public record CountersModule() : IModule
{
    public string Name => "Counter";

    public List<NavItem> NavItems => new List<NavItem>
    {
        new("counter", "Counter", Icons.Material.Filled.Add, NavLinkMatch.Prefix)
    };
}
