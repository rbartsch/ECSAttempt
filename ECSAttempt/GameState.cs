using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ECSAttempt {
    public abstract class GameState {
        public string Name { get; private set; }
        public GameTime GameTime { get; private set; }
        public Camera2D Camera2D { get; private set; }

        private Core core;

        public abstract void Initialize();
        public abstract void Load();
        public abstract void Update();
        public abstract void Draw();
        public abstract void Unload();
        internal void UnloadContent() {
            core.Content.Unload();
        }
        //public abstract void Pause();
        //public abstract void Resume();
        //public abstract void LoadSavedState();

        public GameState(string name) {
            Name = name;
        }

        // We need this because if we create a GameState object with the constructor, if Core
        // is assigned there before engine starts, it will be null and thus never updated. So
        // we can call this when we need to to update the reference.
        internal void RefreshCoreInstance() {
            core = Core.Instance;

            GameTime = core.gameTime;
            Camera2D = core.camera2D;

            Utils.Log("Refreshing core instance");
        }
    }
}