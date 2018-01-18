using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSAttempt.Logger {
    internal class ConsoleLogger : LogBase {
        public override void Log(LogPrefix prefix, LogType type, string message) {
            lock (lockObj) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(string.Format("[{0}::", prefix.ToStringF()));

                switch (type) {
                    case LogType.Info:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(string.Format("{0}", type.ToStringF()));
                        break;

                    case LogType.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(string.Format("{0}", type.ToStringF()));
                        break;

                    case LogType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(string.Format("{0} ", type.ToStringF()));
                        break;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("]\t");
                Console.WriteLine(string.Format("{0}", message));
            }
        }
    }
}