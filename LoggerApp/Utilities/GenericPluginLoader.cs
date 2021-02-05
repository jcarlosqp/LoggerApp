using LogPluginContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LoggerApp.Utilities
{
    public static class GenericPluginLoader<T>
    {
        private const string _PLUGIN_FILE_PATTERN = "*.dll";
        public static ICollection<T> GetPlugins(string path)
        {
            try
            {


                string[] dllFileNames = null;

                if (Directory.Exists(path))
                {
                    dllFileNames = Directory.GetFiles(path, _PLUGIN_FILE_PATTERN);

                    var assemblies = new List<Assembly>(dllFileNames.Length);
                    foreach (string dllFile in dllFileNames)
                    {
                        AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                        Assembly assembly = Assembly.Load(an);
                        assemblies.Add(assembly);

                    }

                    Type pluginType = typeof(T);
                    var pluginTypes = new List<Type>();
                    foreach (var assembly in assemblies)
                    {
                        if (assembly != null)
                        {
                            foreach (Type type in assembly.GetTypes())
                            {
                                if (type.IsInterface || type.IsAbstract)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (type.GetInterface(pluginType.FullName) != null)
                                    {
                                        pluginTypes.Add(type);
                                    }
                                }
                            }
                        }
                    }

                    ICollection<T> myPlugins = new List<T>(pluginTypes.Count);
                    foreach (Type type in pluginTypes)
                    {
                        T plugin = (T)Activator.CreateInstance(type);
                        myPlugins.Add(plugin);
                    }


                    return myPlugins;
                }

                return null;
            }
            catch (Exception ex)
            {

                throw;
            }


        }


    }

}
