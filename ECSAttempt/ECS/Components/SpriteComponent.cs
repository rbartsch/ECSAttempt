using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSAttempt.ECS.Components {
    public class SpriteComponent : BaseComponent {
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public Color Color { get; set; }
        public float LayerDepth { get; set; }

        public string Texture2DPath { get; set; }
        public Texture2D Texture2D { get; set; }
        public Vector2 Origin { get; set; }

        public SpriteComponent(string texture2DPath, float rotation, Vector2 scale, Color color, float layerDepth) {
            Texture2DPath = texture2DPath;
            Rotation = rotation;
            Scale = scale;
            Color = color;
            LayerDepth = layerDepth;
        }
    }
}