using LoggerApp.Parameters;
using LogPluginContract;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace LoggerApp
{

    public static class MyLogger
    {
        private const string _PLUGIN_APPDOMAIN_NAME= "MyPluginsAppDomain";
        private const string _PLUGIN_CONTEXT_KEY = "MyContextKey";
        private static string _PLUGIN_PATH = ConfigurationManager.AppSettings["PluginsPath"];

        public static List<KeyValuePair<string, string>> Logger(LogParameter pLogParam, bool pExecute=false)
        {
            AppDomain pluginsAD = null;
            try
            {

                var pluginAppDomainSetup = new AppDomainSetup();
                pluginAppDomainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;

                pluginsAD = AppDomain.CreateDomain(_PLUGIN_APPDOMAIN_NAME, null, pluginAppDomainSetup);

                var pluginContext = new Utilities.PluginContext();
                pluginContext.FilePath = _PLUGIN_PATH;
                pluginContext.CanExecute = pExecute;
                if (pLogParam!=null)
                {
                    pluginContext.Message = pLogParam.Message;
                    pluginContext.MessageType = pLogParam.MessageType;
                    pluginContext.PluginToExecute = pLogParam.PluginToExecute;
                }
                

                pluginsAD.SetData(_PLUGIN_CONTEXT_KEY, pluginContext);

                pluginsAD.DoCallBack(PluginCallback);

                pluginContext = pluginsAD.GetData(_PLUGIN_CONTEXT_KEY) as Utilities.PluginContext;

                var result=new List<KeyValuePair<string, string>>();
                foreach (var itemPlugin in pluginContext.PluginNames)
                {
                    result.Add(itemPlugin);
                }
               return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción:{ex.Message}");
                return null;
            }
            finally
            {
                AppDomain.Unload(pluginsAD);
            }

        }


        private static void PluginCallback()
        {
            try
            {
                var pluginContext = AppDomain.CurrentDomain.GetData(_PLUGIN_CONTEXT_KEY) as Utilities.PluginContext;
                if (pluginContext != null)
                {
                    int result;
                    var pluginNames=new Dictionary<string, string>();
                    string assemblyQualifiedName;
                    var plugins = Utilities.GenericPluginLoader<ILogPlugin>.GetPlugins(pluginContext.FilePath);
                    foreach (ILogPlugin plugin in plugins)
                    {
                        assemblyQualifiedName = plugin.GetType().AssemblyQualifiedName;
                        pluginNames.Add(assemblyQualifiedName, plugin.LogName);
                        if (pluginContext.CanExecute)
                            if ((pluginContext.PluginToExecute == "*") || (pluginContext.PluginToExecute== assemblyQualifiedName))
                            {
                                result = plugin.Logger(pluginContext.Message, pluginContext.MessageType);
                                if (result > 0)
                                    Console.WriteLine($" - {plugin.LogName}, se ejecutó correctamente.");
                                else
                                    Console.WriteLine($" - {plugin.LogName}, no pudo ejecutarse.");
                            }
                    }
                    pluginContext.PluginNames = pluginNames;
                    pluginContext.CanDelete = true;
                    AppDomain.CurrentDomain.SetData(_PLUGIN_CONTEXT_KEY, pluginContext);

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }



    }
    

}
