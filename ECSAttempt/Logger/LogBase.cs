using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSAttempt.Logger {
    internal abstract class LogBase {
        protected readonly object lockObj = new object();
        public abstract void Log(LogPrefix prefix, LogType type, string message);
    }
}