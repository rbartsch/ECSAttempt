using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECSAttempt;
using ECSAttempt.ECS;
using ECSAttempt.ECS.Components;
using ECSAttempt.ECS.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECSAttemptTest {
    public class UITestState : GameState {
        private EntityManager entityManager;
        private SystemManager systemManager;
        private Text2DSystem text2DSystem;
        private SpriteSystem spriteSystem;
        private ButtonSystem buttonSystem;

        DummyEntity textInfoEntity;

        private Button buttonEntity;
        private Button buttonEntity2;

        public UITestState(string name) : base(name) {
            entityManager = new EntityManager();
            systemManager = new SystemManager();
            text2DSystem = new Text2DSystem();
            spriteSystem = new SpriteSystem();
            buttonSystem = new ButtonSystem();
        }

        public override void Initialize() {
            entityManager.Initialize();

            textInfoEntity = new DummyEntity("text info entity");
            textInfoEntity.GetComponent<PositionComponent>().Position = new Vector3(-650, 300, 0);
            textInfoEntity.AddComponent(new Text2DComponent("Fonts/Terminus", "Buttons are clickable, and change colour upon hovering. Look at the console output to see a response from clicking.\n\nYou can press Q to go back to previous state/screen.", Color.White, Text2DAlignment.Left, false));

            buttonEntity = new Button("ButtonEntity");
            buttonEntity2 = new Button("ButtonEntity2");
            buttonEntity2.GetComponent<PositionComponent>().Position = new Vector3(0, 50, 0);
            buttonEntity2.GetComponent<ButtonComponent>().cameraSpace = false;
            buttonEntity2.GetComponent<ButtonComponent>().Click += ButtonTestPrint;
            buttonEntity.GetComponent<ButtonComponent>().Click += delegate (object s, EventArgs e) {
                ButtonTestPrintNum(s, e, new Random().Next(0, 1000));
            };
        }

        void ButtonTestPrint(object sender, EventArgs e) {
            Utils.Log("BUTTON TEST!");
        }

        void ButtonTestPrintNum(object sender, EventArgs e, int n) {
            Utils.Log("BUTTON TEST: " + n);
        }

        public override void Load() {
            EntityManager.Instantiate(buttonEntity);
            EntityManager.Instantiate(buttonEntity2);
            EntityManager.Instantiate(textInfoEntity);

            text2DSystem.Load();
            spriteSystem.Load();
            buttonSystem.Load();
        }

        public override void Update() {
            // Press Q to load UITestState
            if (KeyInput.KeyPress(Keys.Q)) {
                GameStateManager.ChangeState(new TestState("TestState"));
            }

            text2DSystem.Update();
            buttonSystem.Update(Mouse.GetState(), Camera2D);
            entityManager.Update(GameTime);
        }

        public override void Draw() {
            text2DSystem.Draw();
            spriteSystem.Draw();
        }

        public override void Unload() {
        }
    }
}