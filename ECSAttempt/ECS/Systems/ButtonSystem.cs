using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECSAttempt.ECS.Components;

namespace ECSAttempt.ECS.Systems {
    public class ButtonSystem : BaseSystem {
        public Dictionary<BaseEntity, ValueTuple<ButtonComponent, SpriteComponent, PositionComponent>> entities;

        public ButtonSystem() {
            entities = new Dictionary<BaseEntity, (ButtonComponent, SpriteComponent, PositionComponent)>();
            SystemManager.Add(this);
        }

        (ButtonComponent but, SpriteComponent spr, PositionComponent pos) GetComponentsAsTuple(ButtonComponent b, SpriteComponent s, PositionComponent p) {
            var but = b;
            var spr = s;
            var pos = p;
            return (but, spr, pos);
        }

        public override void SyncInterestedEntities(bool remove) {
            // TODO: Have a look at an alternative implementation to be done in SystemManager to automatically iterate upon small list of types. This applies to all Systems
            if (LatestEntityHasComponent<ButtonComponent>() && LatestEntityHasComponent<SpriteComponent>()) {
                BaseEntity e = GetEntity(entities);
                if (e != null) {
                    entities.Add(e, (GetComponent<ButtonComponent>(), GetComponent<SpriteComponent>(), GetComponent<PositionComponent>()));
                }
            }

            if (remove) {
                List<BaseEntity> entitiesRemovable = GetListOfRemovableEntitiesOf<ButtonComponent>();
                entitiesRemovable.Union(GetListOfRemovableEntitiesOf<SpriteComponent>());
                entitiesRemovable.Union(GetListOfRemovableEntitiesOf<PositionComponent>());
                RemoveEntities(entities, entitiesRemovable);
            }

            Load();
        }

        public void Load() {
            foreach (KeyValuePair<BaseEntity, ValueTuple<ButtonComponent, SpriteComponent, PositionComponent>> p in entities) {
                if (p.Key.instantiated) {
                    var v = GetComponentsAsTuple(p.Value.Item1, p.Value.Item2, p.Value.Item3);

                    v.but.ButtonRect = new Rectangle((int)v.pos.Position.X - v.spr.Texture2D.Bounds.Width / 2, (int)v.pos.Position.Y - v.spr.Texture2D.Bounds.Height / 2, v.spr.Texture2D.Bounds.Width, v.spr.Texture2D.Bounds.Height);

                    if (v.but.cameraSpace) {
                        v.but.cameraSpaceOffset = new Vector3(v.pos.Position.X, v.pos.Position.Y, v.pos.Position.Z);
                    }
                }
            }
        }

        MouseState oldMouseState;
        public void Update(MouseState mouseState, Camera2D camera2D) {
            foreach (KeyValuePair<BaseEntity, ValueTuple<ButtonComponent, SpriteComponent, PositionComponent>> p in entities) {
                var v = GetComponentsAsTuple(p.Value.Item1, p.Value.Item2, p.Value.Item3);

                if (!v.but.cameraSpace) {
                    v.pos.Position = new Vector3(v.pos.Position.X, v.pos.Position.Y, v.pos.Position.Z);
                }
                else {
                    v.pos.Position = new Vector3(Core.Instance.camera2D.VisibleArea.Center.X + v.but.cameraSpaceOffset.X, -Core.Instance.camera2D.VisibleArea.Center.Y + v.but.cameraSpaceOffset.Y, v.pos.Position.Z);
                    v.but.ButtonRect = new Rectangle((int)v.pos.Position.X - v.spr.Texture2D.Bounds.Width / 2, (int)v.pos.Position.Y - v.spr.Texture2D.Bounds.Height / 2, v.spr.Texture2D.Bounds.Width, v.spr.Texture2D.Bounds.Height);
                }

                // if we try check here if button HasListener then we don't highlight
                // button color, then in else we can show disabled color since no listener is
                // attached any ways.

                // Transform Mouse.GetState() mouse pos which is screen position to world position
                // if the UI is in world position and not screen
                Matrix inverseTransform = Matrix.Invert(camera2D.Transform);
                Vector2 mouseInWorld = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), inverseTransform);
                mouseInWorld = new Vector2(mouseInWorld.X, -mouseInWorld.Y);

                v.but.MouseRect = new Rectangle((int)mouseInWorld.X, (int)mouseInWorld.Y, v.but.MouseRect.Width, v.but.MouseRect.Height);
                if (v.but.ButtonRect.Intersects(v.but.MouseRect)) {
                    v.spr.Color = v.but.HoverColor;

                    if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released) {
                        if (v.but.HasListeners()) {
                            v.spr.Color = v.but.ClickColor;
                            v.but.OnClick(new EventArgs());
                        }
                    }

                    oldMouseState = mouseState;
                }
                else {
                    v.spr.Color = v.but.DefaultColor;
                }
            }
        }
    }
}