using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECSAttempt.Logger;

namespace ECSAttempt {
    internal static class Utils {
        private static LogHelper logHelper = new LogHelper();

        public static void Log(string message, LogType logType = LogType.Info) {
            logHelper.Log(LogTarget.Console, LogPrefix.Engine, logType, message);
        }
    }
}