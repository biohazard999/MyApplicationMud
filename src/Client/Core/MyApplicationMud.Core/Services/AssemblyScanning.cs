
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MyApplicationMud.Services;

public static class AssemblyScanning
{
    public static List<Assembly> GetAssemblies(IJSRuntime jsRuntime, string startsWith = "", string endsWith = "")
    {
        var allAssemblies = new List<Assembly>();

        var refs = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var reference in refs.Where(x => !x.IsDynamic))
        {
            var name = reference.GetName().Name;
            if (name is not null && name.StartsWith(startsWith) && name.EndsWith(endsWith))
            {
                allAssemblies.Add(reference);
            }
        }

        var returnAssemblies = allAssemblies
            .Where(w => w.GetTypes().Any(a => a.GetInterfaces().Contains(typeof(IModule))));

        return returnAssemblies.ToList();
    }

    public static IEnumerable<TypeInfo> GetTypes<T>(IJSRuntime jsRuntime, string startsWith = "", string endsWith = "")
        => GetAssemblies(jsRuntime, startsWith, endsWith)
        .SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));

    public static IEnumerable<T> GetInstances<T>(IJSRuntime jsRuntime, string startsWith = "", string endsWith = "")
    {
        var instances = new List<T>();

        foreach (Type implementation in GetTypes<T>(jsRuntime, startsWith, endsWith))
        {
            if (!implementation.GetTypeInfo().IsAbstract)
            {
                var instance = Activator.CreateInstance(implementation);
                if (instance is T tInstance)
                {
                    instances.Add(tInstance);
                }
                else
                {
                    throw new Exception($"Can not resolve {typeof(T).FullName} from type {implementation.FullName}");
                }
            }
        }

        return instances;
    }

}


