using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSAttempt.Logger {
    public class LogHelper {
        private LogBase logger = null;

        public void Log(LogTarget target, LogPrefix prefix, LogType type, string message) {
            switch (target) {
                case LogTarget.Console:
                    logger = new ConsoleLogger();
                    break;
                case LogTarget.File:
                    logger = new FileLogger();
                    break;
                default:
                    return;
            }

            logger.Log(prefix, type, message);
        }
    }
}