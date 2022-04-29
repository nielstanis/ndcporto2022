using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace ConsoleApp
{
    class IsolatedLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyName[] _sharedAssemblies;
        private readonly AssemblyDependencyResolver _resolver;
        private readonly string _assemblyLoadPath;

        private readonly bool _fallbackManagedResolving;
        private readonly bool _fallbackUnmanagedResolving;
        
        public IsolatedLoadContext(string assemblyLoadPath, Type[] sharedTypes, bool fallbackManagedResolving, bool fallbackUnmanagedResolving)
        {
            _assemblyLoadPath = assemblyLoadPath;
            _resolver = new AssemblyDependencyResolver(assemblyLoadPath);

            _fallbackManagedResolving = fallbackManagedResolving;
            _fallbackUnmanagedResolving = fallbackUnmanagedResolving;

            _sharedAssemblies = new AssemblyName[sharedTypes.Length];
            for (int i = 0; i < sharedTypes.Length; i++)
            {
                _sharedAssemblies[i] = sharedTypes[i].Assembly.GetName();
            }
        }

        public IsolatedLoadContext(string assemblyLoadPath, Type[] sharedTypes) : this(assemblyLoadPath, sharedTypes, false, true)
        {}

        public IsolatedLoadContext(string assemblyLoadPath, Type[] sharedTypes, bool fallbackManagedResolving) : this(assemblyLoadPath, sharedTypes, fallbackManagedResolving, true)
        {}

        protected override Assembly Load(AssemblyName assemblyName)
        {
            //check for shared assemblies, return null because they'll be loaded by default AssemblyLoadContext 
            if (_sharedAssemblies.FirstOrDefault(x=>x.FullName == assemblyName.FullName)!=null)
            {
                return null;
            }

            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            //Do we want the default AssemblyLoadContext to help out with resolving managed assembly?
            if (_fallbackManagedResolving) 
            {
                return null; 
            }

            throw new NotSupportedException($"Unable to load assembly: {assemblyName.Name}");
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }
            
            //Do we want the default AssemblyLoadContext to help out with resolving unmanaged assembly?
            if (_fallbackUnmanagedResolving)
            {
                return IntPtr.Zero;
            }

            throw new NotSupportedException($"Unable to load assembly: {unmanagedDllName}");
        }

        public T CreateInstance<T>(string assemblyToLoad)
        {
            T result = default(T);
            var loadedAssembly = this.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyToLoad)));
            var types = loadedAssembly.GetTypes();
            foreach (Type type in types)
            {
                if (typeof(T).IsAssignableFrom(type))
                {
                    result = (T)Activator.CreateInstance(type);
                    break;
                }
            } 
            return result;
        }

        public T CreateInstance<T>()
        {
            return CreateInstance<T>(_assemblyLoadPath);
        }
    }

}