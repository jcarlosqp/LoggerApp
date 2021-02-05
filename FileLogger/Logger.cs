using LogPluginContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace FileLogger
{
    
    public class Logger : ILogPlugin
    {
        public string LogName => "File Logger";
        private string _FullFilePath;
        public Logger()
        {
            _FullFilePath = ConfigurationManager.AppSettings["LogFilePath"];
        }
        int ILogPlugin.Logger(string pMessage, MessageTypeEnum pTypeMessage)
        {
            try
            {
                string[] lines = new string[1];
                lines[0] = $"\n[Hora: {DateTime.Now.ToString()}] - Mensage: {pMessage} | Tipo: {pTypeMessage.ToString()}\n";
                File.AppendAllLines(_FullFilePath, lines);

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }

        }
    }
}
