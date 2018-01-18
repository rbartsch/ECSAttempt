using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSAttempt.ECS.Components {
    public class Text2DComponent : BaseComponent {
        public string Text { get; set; }
        public Color Color { get; set; }
        public Text2DAlignment Alignment { get; set; }

        public string fontPath;
        public SpriteFont spriteFont;
        public Vector3 origin;
        public bool cameraSpace;
        public Vector3 cameraSpaceOffset;

        public Text2DComponent(string fontPath, string text, Color? color = null, Text2DAlignment alignment = Text2DAlignment.Left, bool cameraSpace = true) {
            this.fontPath = fontPath;
            Text = text;
            Color = color ?? Color.White;
            Alignment = alignment;
            this.cameraSpace = cameraSpace;
        }
    }
}
