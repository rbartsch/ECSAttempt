using System;
using ECSAttempt;
using ECSAttempt.ECS;
using ECSAttempt.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECSAttemptTest
{
    public class DummyEntity : BaseEntity {
        Text2DComponent txt;
        PositionComponent pos;

        public DummyEntity(string id) : base(id) {
            AddComponent(new PositionComponent(new Vector3(0, 0, 0)));
            pos = GetComponent<PositionComponent>();
        }

        public override void Update() {

        }
    }
}
