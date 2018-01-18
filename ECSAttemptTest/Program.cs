using System;

namespace ECSAttemptTest {
#if WINDOWS || LINUX
    public static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            TestGame game = new TestGame();
            game.Start();
        }
    }
#endif
}