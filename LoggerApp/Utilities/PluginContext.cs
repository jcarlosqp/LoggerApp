using LogPluginContract;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LoggerApp.Utilities
{
    [Serializable]
    public class PluginContext
    {
        public string FilePath { get; set; }

        public string Message { get; set; }

        public bool CanDelete { get; set; }

        public bool CanExecute { get; set; }

        public MessageTypeEnum MessageType { get; set; }

        public IDictionary<string, string> PluginNames { get; set; }

        public string PluginToExecute { get; set; }
    }
}
