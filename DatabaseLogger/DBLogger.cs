using LogPluginContract;
using System;
using DatabaseLogger.Service;

namespace DatabaseLogger
{
    public class DBLogger : ILogPlugin
    {
        public string LogName => "Database Logger";

        public int Logger(string pMessage, MessageTypeEnum pTypeMessage)
        {
            return LogService.Log(pMessage, pTypeMessage);
        }
    }
}
