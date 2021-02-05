using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogPluginContract
{
    public interface ILogPlugin
    {
        string LogName { get; }

        int Logger(string pMessage, MessageTypeEnum pTypeMessage);
    }
}
