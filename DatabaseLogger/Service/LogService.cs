using LogPluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLogger.Service
{
    public static class LogService
    {
        public static int Log(string pMessage, MessageTypeEnum pTypeMessage)
        {
            try
            {

                using (var ctx = new Model.LogModelContainer())
                {
                    var newLog = new Model.Log() {
                        Message = pMessage,
                        Type = (int)pTypeMessage
                    };
                    ctx.LogSet.Add(newLog);
                    ctx.SaveChanges();
                }

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int CountLogRows()
        {
            try
            {
                var count = -1;
                using (var ctx = new Model.LogModelContainer())
                {
                    count=ctx.LogSet.Count();
                }

                return count;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

    }
}
