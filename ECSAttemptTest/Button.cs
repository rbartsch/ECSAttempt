using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECSAttempt;
using ECSAttempt.ECS;
using ECSAttempt.ECS.Components;
using Microsoft.Xna.Framework;

namespace ECSAttemptTest
{
    public class Button : BaseEntity {
        SpriteComponent spr;
        PositionComponent pos;
        ButtonComponent but;

        public Button(string name) : base(name) {
            AddComponent(new SpriteComponent("Textures/button", 0, new Vector2(1, 1), Color.White, 0));
            AddComponent(new PositionComponent(new Vector3(0, 0, 0)));
            AddComponent(new ButtonComponent(Color.White, Color.Yellow, Color.Transparent));
        }
    }
}
