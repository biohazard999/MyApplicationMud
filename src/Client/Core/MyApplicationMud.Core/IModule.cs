using System;
using System.Collections.Generic;

namespace MyApplicationMud;

public interface IModule
{
    string Name { get; }

    List<NavItem> NavItems { get; }
}
