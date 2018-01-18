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

/// <summary>
/// NOTE:
/// If you try access this class from another project you won't be able to access derived
/// Game members, only public members of this class. This is because MonoGame types such as GameTime
/// would not be possible in the other project because that type doesn't exist. In order to do so 
/// you'd need to include MonoGame.Framework reference in the other project.
/// </summary>
namespace ECSAttempt {
    internal sealed class Core : Game {
        public SpriteBatch spriteBatch;
        public GraphicsDeviceManager graphicsDeviceManager;
        public GameTime gameTime;
        public Camera2D camera2D;
        public static Core Instance { get { return _instance; } }

        internal bool warmUpDone = false;
        
        private static readonly Core _instance = new Core();

        Core() {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
        }

        protected override void Initialize() {
            //if (!graphicsDeviceManager.IsFullScreen)
            //    Window.Position = new Point((GraphicsDevice.DisplayMode.Width / 2) - graphicsDeviceManager.PreferredBackBufferWidth / 2, (GraphicsDevice.DisplayMode.Height / 2) - graphicsDeviceManager.PreferredBackBufferHeight / 2);

            camera2D = new Camera2D(GraphicsDevice.Viewport);
            GameStateManager.InitializeGameState();

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameStateManager.LoadGameState();

            base.LoadContent();
        }

        protected override void Update(GameTime gt) {
            if (warmUpDone) {
                gameTime = gt;

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                camera2D.Update(GraphicsDevice.Viewport, gameTime);
                GameStateManager.UpdateGameState();
                KeyInput.UpdateKeyState();

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gt) {
            if (warmUpDone) {
                gameTime = gt;

                GraphicsDevice.Clear(new Color(38, 33, 68, 255));
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null, null, camera2D.Transform);
                // Draw ...
                GameStateManager.DrawGameState();
                spriteBatch.End();

                // Have two spritebatches, one for world space, one for screen space (don't give camera transform?)

                base.Draw(gameTime);
            }
        }

        protected override void UnloadContent() {
            GameStateManager.UnloadGameState();

            base.UnloadContent();
        }
    }
}