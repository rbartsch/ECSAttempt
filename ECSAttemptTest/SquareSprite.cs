using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECSAttempt;
using ECSAttempt.ECS;
using ECSAttempt.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECSAttemptTest {
    public class SquareSprite : BaseEntity {
        SpriteComponent square;
        PositionComponent pos;

        public SquareSprite(string id) : base(id) {
            AddComponent(new SpriteComponent("Textures/square", 0, new Vector2(1, 1), Color.White, 0));
            AddComponent(new PositionComponent(new Vector3(0, 0, 0)));
            square = GetComponent<SpriteComponent>();
            pos = GetComponent<PositionComponent>();

        }

        public override void Update() {
            // Move sprite entities, will reduce FPS at 3k entities spawned, this causes it the KeyInput.KeyHold... but this is just a test.
            if (KeyInput.KeyHold(Keys.W)) {
                pos.Position = pos.Position + new Vector3(0, 100 * (float)GameTime.ElapsedGameTime.TotalSeconds, 0);
                pos.Position = new Vector3((float)Math.Round(pos.Position.X), (float)Math.Round(pos.Position.Y), 0);
            }

            if (KeyInput.KeyHold(Keys.A)) {
                pos.Position = pos.Position + new Vector3(-100 * (float)GameTime.ElapsedGameTime.TotalSeconds, 0, 0);
                pos.Position = new Vector3((float)Math.Round(pos.Position.X), (float)Math.Round(pos.Position.Y), 0);
            }

            if (KeyInput.KeyHold(Keys.S)) {
                pos.Position = pos.Position + new Vector3(0, -100 * (float)GameTime.ElapsedGameTime.TotalSeconds, 0);
                pos.Position = new Vector3((float)Math.Round(pos.Position.X), (float)Math.Round(pos.Position.Y), 0);
            }

            if (KeyInput.KeyHold(Keys.D)) {
                pos.Position = pos.Position + new Vector3(100 * (float)GameTime.ElapsedGameTime.TotalSeconds, 0, 0);
                pos.Position = new Vector3((float)Math.Round(pos.Position.X), (float)Math.Round(pos.Position.Y), 0);
            }
        }
    }
}