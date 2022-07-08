using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.Routing;

namespace MyApplicationMud;

public record WeatherModule() : IModule
{
    public string Name => "Weather";

    public List<NavItem> NavItems => new List<NavItem>
    {
        new("fetchdata", "Weather", Icons.Material.Filled.List, NavLinkMatch.Prefix)
    };
}
