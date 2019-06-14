using NLog;
using System.Reflection;

namespace zg.frontsite
{
    public class LogProvider
    {
        public static readonly Logger Console;

        public static readonly Logger Info;

        public static readonly Logger Debug;

        public static readonly Logger Error;

        static LogProvider()
        {
            Console = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.FullName + ".ColoredConsole");
            Info = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.FullName + ".Info");
            Debug = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.FullName + ".Debug");
            Error = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.FullName + ".Error");
        }
    }
}