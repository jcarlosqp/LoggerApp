using LogPluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLogger
{
    
    public class Logger : ILogPlugin
    {
        public string LogName => "Console Logger";

        int ILogPlugin.Logger(string pMessage, MessageTypeEnum pTypeMessage)
        {
            try
            {
                string texto = $"\n[Hora: {DateTime.Now.ToString()}] - Mensage: {pMessage} | Tipo: {pTypeMessage.ToString()}";
                Console.WriteLine(texto);

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }
    }
}
