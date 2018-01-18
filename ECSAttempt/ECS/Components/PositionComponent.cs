using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ECSAttempt.ECS.Components {
    public class PositionComponent : BaseComponent
    {
        public Vector3 Position { get; set; }

        public PositionComponent(Vector3 pos) {
            Position = pos;
        }
    }
}
