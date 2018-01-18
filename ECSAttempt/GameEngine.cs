using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSAttempt {
    public class GameEngine {
        // Can be made public and Core changed to public instead of internal if we ever had to expose 
        // it to the game
        private Core core;

        public GameEngine() {
            core = Core.Instance;
            core.IsFixedTimeStep = false;
            core.IsMouseVisible = true;
        }

        public void WarmUp() {
            core.RunOneFrame();
            core.warmUpDone = true;
        }

        public void Start() {
            using (core) {
                // maybe RunOneFrame() can be used to first initialize everything
                // e.g for things like GraphicsDevice being null until Initialize() is called
                //core.RunOneFrame();
                core.Run();
                // nothing after this can be called because it's in a loop
            }
        }

        public void SetResolution(int width, int height, bool fullscreen) {
            core.graphicsDeviceManager.PreferredBackBufferWidth = width;
            core.graphicsDeviceManager.PreferredBackBufferHeight = height;
            core.graphicsDeviceManager.IsFullScreen = fullscreen;
            core.graphicsDeviceManager.ApplyChanges();
            //if (!core.graphicsDeviceManager.IsFullScreen && core.GraphicsDevice != null)
            //    core.Window.Position = new Point((core.GraphicsDevice.DisplayMode.Width / 2) - core.graphicsDeviceManager.PreferredBackBufferWidth / 2, (core.GraphicsDevice.DisplayMode.Height / 2) - core.graphicsDeviceManager.PreferredBackBufferHeight / 2);
            if (!core.graphicsDeviceManager.IsFullScreen)
                core.Window.Position = new Point((core.GraphicsDevice.DisplayMode.Width / 2) - core.graphicsDeviceManager.PreferredBackBufferWidth / 2, (core.GraphicsDevice.DisplayMode.Height / 2) - core.graphicsDeviceManager.PreferredBackBufferHeight / 2);
        }

        public string GetContentRoot() {
            return core.Content.RootDirectory;
        }

        public void SetContentRoot(string dir) {
            core.Content.RootDirectory = dir;
        }
    }
}