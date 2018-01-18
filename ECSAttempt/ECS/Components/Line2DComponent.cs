using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSAttempt.ECS.Components
{
    public class Line2DComponent : BaseComponent
    {
        public Vector2 EndPosition { get; set; }
        public Vector2 Edge { get; set; }
        public Color Color { get; set; }
        public int Width { get; set; }
        public float Angle { get; set; }
        public float LayerDepth { get; set; }

        public Texture2D Texture2D { get; set; }

        public Line2DComponent(Vector2 endPos, Color color, int width, float layerDepth) {
            EndPosition = endPos;
            Color = color;
            Width = width;
            LayerDepth = layerDepth;
        }
    }
}
