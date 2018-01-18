using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ECSAttempt;
using ECSAttempt.ECS;
using ECSAttempt.ECS.Components;
using ECSAttempt.ECS.Systems;
using System.Diagnostics;

namespace ECSAttemptTest {
    public class TestState : GameState {
        private EntityManager entityManager;
        private SystemManager systemManager;
        private SpriteSystem spriteSystem;
        private Line2DSystem line2DSystem;
        private Text2DSystem text2DSystem;

        private SquareSprite squareSprite;
        private SquareSprite squareSprite2;

        private DummyEntity textInfoEntity;

        public TestState(string name) : base(name) {
            entityManager = new EntityManager();
            systemManager = new SystemManager();
            spriteSystem = new SpriteSystem();
            line2DSystem = new Line2DSystem();
            text2DSystem = new Text2DSystem();
        }

        public override void Initialize() {
            //throw new NotImplementedException();
            entityManager.Initialize();
            line2DSystem.Initialize();

            textInfoEntity = new DummyEntity("text info entity");
            textInfoEntity.GetComponent<PositionComponent>().Position = new Vector3(-650, 300, 0);
            textInfoEntity.AddComponent(new Text2DComponent("Fonts/Terminus", "You can move the camera around by pressing the Arrow keys.\nYou can move some objects on screen around by pressing W, A, S, or D." +
                "\n\nYou can press U to spawn 50 more entities that has text, sprite image, and line components.\n You can press P to remove entities at random.\n\nYou can press Q to go to load another state/screen to see the Button System", Color.White, Text2DAlignment.Left, false));

            squareSprite = new SquareSprite("SquareSprite");
            squareSprite2 = new SquareSprite("SquareSprite2");
            squareSprite2.GetComponent<PositionComponent>().Position = new Vector3(100, 100, 0);
            squareSprite2.AddComponent(new Text2DComponent("Fonts/Terminus", "THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG.\nThe quick brown fox jumps over the lazy dog.\n1234567890", Color.White, Text2DAlignment.Left, false));
        }

        public override void Load() {
            //throw new NotImplementedException();
            EntityManager.Instantiate(textInfoEntity);
            EntityManager.Instantiate(squareSprite);
            EntityManager.Instantiate(squareSprite2);

            spriteSystem.Load();
            text2DSystem.Load();
        }

        public override void Update() {
            //throw new NotImplementedException();

            //if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
            //    Utils.Log("Pressed left button");
            //}
            //if(Mouse.GetState().LeftButton == ButtonState.Released) {
            //    Utils.Log("Released left button");
            //}

            // Press U to spawn entities and add components
            if (KeyInput.KeyPress(Keys.U)) {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Random r = new Random();
                for (int i = 0; i < 50; i++) {
                    SquareSprite s = new SquareSprite("S");

                    s.GetComponent<PositionComponent>().Position = new Vector3(r.Next(-500, 500), r.Next(-500, 500), 0);
                    s.AddComponent(new Line2DComponent(new Vector2(50, 50), Color.White, 1, 0));
                    s.AddComponent(new Text2DComponent("Fonts/Terminus", "THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG.\nThe quick brown fox jumps over the lazy dog.\n1234567890", Color.White, Text2DAlignment.Left, false));
                    EntityManager.Instantiate(s);
                }

                sw.Stop();
                Utils.Log("Took: " + sw.Elapsed.TotalSeconds);
            }

            // Press O to remove components from entities at random (the random is inefficient but useful for a quick test)
            if (KeyInput.KeyPress(Keys.O)) {
                Random r = new Random();
                int rem = r.Next(0, EntityManager.entities.Count);
                Utils.Log("Removed at random component from entity");
                SpriteComponent c = EntityManager.entities[rem].GetComponent<SpriteComponent>();
                if (c != null) {
                    EntityManager.entities[rem].RemoveComponent(c);
                }
            }

            // Press P to remove entities at random (the random is inefficient but useful for a quick test)
            if (KeyInput.KeyPress(Keys.P)) {
                if (EntityManager.entities.Count > 0) {
                    Random r = new Random();
                    int rem = r.Next(0, EntityManager.entities.Count);
                    Utils.Log("Removed at random an entity");
                    BaseEntity e = EntityManager.entities[rem];
                    if (e != null) {
                        EntityManager.Destroy(e);
                    }
                }
            }

            // Press Q to load UITestState
            if (KeyInput.KeyPress(Keys.Q)) {
                GameStateManager.ChangeState(new UITestState("UITestState"));
            }

            text2DSystem.Update();
            entityManager.Update(GameTime);
        }

        public override void Draw() {
            //throw new NotImplementedException();
            spriteSystem.Draw();
            line2DSystem.Draw();
            text2DSystem.Draw();
        }

        public override void Unload() {
            //throw new NotImplementedException();
            line2DSystem.Dispose();
        }
    }
}