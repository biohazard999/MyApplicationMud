using Microsoft.AspNetCore.Components.Routing;

namespace MyApplicationMud;

public record NavItem(string Url, string Name, string Icon, NavLinkMatch NavLinkMatch);
