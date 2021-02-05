using LogPluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerApp.Parameters
{
    public class LogParameter
    {
        
        public string Message { get; set; }

        public MessageTypeEnum MessageType { get; set; }

        public string PluginToExecute { get; set; }
    }
}
