namespace RabbitmqDotNetCore
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Diagnostics;

    public interface IAssembliesProvider
    {
        IEnumerable<Assembly> Assemblies { get; }
    }

    public class AssembliesProvider : IAssembliesProvider
    {
        private static readonly IAssembliesProvider Singleton = new AssembliesProvider();

        private static readonly object SyncLock = new object();
        private static IEnumerable<Assembly> assemblies;

        public static IAssembliesProvider Instance
        {
            get { return Singleton; }
        }

        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (assemblies != null)
                {
                    return assemblies;
                }

                var available = AvailableAssemblies();

                lock (SyncLock)
                {
                    if (assemblies == null)
                    {
                        assemblies = available;
                    }
                }

                return assemblies;
            }
        }

        private static IEnumerable<Assembly> AvailableAssemblies()
        {
            return ConsoleAssemblies();
        }

        private static IEnumerable<Assembly> ConsoleAssemblies()
        {
            var path = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            if (path == null)
            {
                throw new InvalidOperationException("Unable to get directory location.");
            }

            var filePattern = "*.dll";

            var files = Directory.GetFiles(path, filePattern)
                .Where(item => IsValidAssemlyFile(Path.GetFileNameWithoutExtension(item)));
            

            var allAssemblies = files.Select(LoadAssemblyFile).ToList();

            allAssemblies.Add(Assembly.GetEntryAssembly());

            var result = allAssemblies.Where(a => a != null).Distinct().OrderBy(a => a.FullName).ToList();
            return result;
        }

        private static bool IsValidAssemlyFile(string assemlyFileName)
        {
            return GlobalDictionary.AssemblyNames.Any(
                item => assemlyFileName.StartsWith(item, StringComparison.OrdinalIgnoreCase));
        }
        private static Assembly LoadAssemblyFile(string path)
        {
            try
            {
                return Assembly.LoadFile(path);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error while loading asembly file: {0}. Exception: '{1}'. {2}", path, ex.GetType().FullName, ex.Message);
                throw;
            }
        }
    }
}