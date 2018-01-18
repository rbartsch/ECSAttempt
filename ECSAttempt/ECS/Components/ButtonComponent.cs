using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ECSAttempt.ECS.Components {
    public class ButtonComponent : BaseComponent {
        public Rectangle ButtonRect { get; set; }
        public Rectangle MouseRect { get; set; }
        public Color DefaultColor { get; set; }
        public Color HoverColor { get; set; }
        public Color ClickColor { get; set; }

        public event EventHandler Click;
        public bool cameraSpace;
        public Vector3 cameraSpaceOffset;

        public ButtonComponent(Color defaultColor, Color hoverColor, Color clickColor, bool cameraSpace = true) {
            ButtonRect = new Rectangle();
            MouseRect = new Rectangle(0, 0, 2, 2);
            DefaultColor = defaultColor;
            HoverColor = hoverColor;
            ClickColor = clickColor;
            this.cameraSpace = cameraSpace;
        }

        public void OnClick(EventArgs e) {
            Click?.Invoke(this, e);
        }

        public bool HasListeners() {
            if (Click != null && Click.GetInvocationList().Length > 0) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}