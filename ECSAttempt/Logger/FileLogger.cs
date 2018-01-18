using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSAttempt.Logger {
    internal class FileLogger : LogBase {
        public string file = "log.txt";

        public override void Log(LogPrefix prefix, LogType type, string message) {
            lock (lockObj) {
                using (StreamWriter sw = new StreamWriter(file, true)) {
                    string f;
                    if (type == LogType.Error)
                        f = string.Format("[{0}::{1} ]", prefix.ToStringF(), type.ToStringF());
                    else
                        f = string.Format("[{0}::{1}]", prefix.ToStringF(), type.ToStringF());

                    sw.WriteLine(string.Format("{0}\t{1}", f, message));
                    sw.Close();
                }
            }
        }
    }
}