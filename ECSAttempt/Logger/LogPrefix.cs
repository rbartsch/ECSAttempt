using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSAttempt.Logger {
    public enum LogPrefix {
        Engine,
        Game,
        General
    }

    internal static class LogPrefixExtensions {
        internal static string ToStringF(this LogPrefix logPref) {
            switch (logPref) {
                case LogPrefix.Engine:
                    return "ENGINE";

                case LogPrefix.Game:
                    return "GAME";

                case LogPrefix.General:
                    return "GENERAL";

                default:
                    throw new System.ArgumentException();
            }
        }
    }
}