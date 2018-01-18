using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSAttempt.Logger {
    public enum LogType {
        Info,
        Warning,
        Error
    }

    internal static class LogTypeExtensions {
        internal static string ToStringF(this LogType logType) {
            switch (logType) {
                case LogType.Info:
                    return "INFO";

                case LogType.Warning:
                    return "WARN";

                case LogType.Error:
                    return "ERR";

                default:
                    throw new System.ArgumentException();
            }
        }
    }
}