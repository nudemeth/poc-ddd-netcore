using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Plugins
{
    public class InfrastructurePluginLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver resolver;

        private InfrastructurePluginLoadContext(string pluginPath)
        {
            resolver = new AssemblyDependencyResolver(pluginPath);
        }

        public static Assembly LoadPlugin(string path)
        {
            var directoy = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
            var loadContext = new InfrastructurePluginLoadContext($"{directoy}/{path}");

            return loadContext.LoadFromAssemblyName(AssemblyName.GetAssemblyName($"{directoy}/{path}"));
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            if (Default.Assemblies.Any(a => a.FullName == assemblyName.FullName))
            {
                return null;
            }

            var assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            var libraryPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
    }
}
