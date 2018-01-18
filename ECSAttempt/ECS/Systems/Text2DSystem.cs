using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECSAttempt.ECS.Components;

namespace ECSAttempt.ECS.Systems {
    public class Text2DSystem : BaseSystem {
        public Dictionary<BaseEntity, ValueTuple<Text2DComponent, PositionComponent>> entities;
        
        public Text2DSystem() {
            entities = new Dictionary<BaseEntity, (Text2DComponent, PositionComponent)>();
            SystemManager.Add(this);
        }

        (Text2DComponent text, PositionComponent pos) GetComponentsAsTuple(Text2DComponent t, PositionComponent p) {
            var text = t;
            var pos = p;
            return (text, pos);
        }

        public override void SyncInterestedEntities(bool remove) {
            if(LatestEntityHasComponent<Text2DComponent>() && LatestEntityHasComponent<PositionComponent>()) {
                BaseEntity e = GetEntity(entities);
                if(e != null) {
                    entities.Add(e, (GetComponent<Text2DComponent>(), GetComponent<PositionComponent>()));
                }
            }

            if (remove) {
                List<BaseEntity> entitiesRemovable = GetListOfRemovableEntitiesOf<Text2DComponent>();
                entitiesRemovable.Union(GetListOfRemovableEntitiesOf<PositionComponent>());
                RemoveEntities(entities, entitiesRemovable);
            }

            Load();
        }

        public void Load() {
            foreach(KeyValuePair<BaseEntity, ValueTuple<Text2DComponent, PositionComponent>> p in entities) {
                if (p.Key.instantiated) {
                    var v = GetComponentsAsTuple(p.Value.Item1, p.Value.Item2);

                    v.text.spriteFont = Core.Instance.Content.Load<SpriteFont>(v.text.fontPath);
                    switch (v.text.Alignment) {
                        case Text2DAlignment.Left:
                            v.text.origin = new Vector3(0, 0, 0);
                            break;
                        case Text2DAlignment.Center:
                            v.text.origin.X = v.text.spriteFont.MeasureString(v.text.Text).X / 2;
                            break;
                        case Text2DAlignment.Right:
                            v.text.origin.X = v.text.spriteFont.MeasureString(v.text.Text).X;
                            break;
                        default:
                            break;
                    }

                    if (v.text.cameraSpace) {
                        v.text.cameraSpaceOffset = new Vector3(v.pos.Position.X, v.pos.Position.Y, v.pos.Position.Z);
                    }
                }
            }
        }

        public void Draw() {
            foreach (KeyValuePair<BaseEntity, ValueTuple<Text2DComponent, PositionComponent>> p in entities) {
                var v = GetComponentsAsTuple(p.Value.Item1, p.Value.Item2);

                if (v.text.spriteFont != null)
                    Core.Instance.spriteBatch.DrawString(v.text.spriteFont, v.text.Text, new Vector2(v.pos.Position.X, -v.pos.Position.Y), v.text.Color, 0, new Vector2(v.text.origin.X, v.text.origin.Y), 1, SpriteEffects.None, 0.0f);
            }
        }

        public void Update() {
            foreach (KeyValuePair<BaseEntity, ValueTuple<Text2DComponent, PositionComponent>> p in entities) {
                var v = GetComponentsAsTuple(p.Value.Item1, p.Value.Item2);

                if (!v.text.cameraSpace) {
                    v.pos.Position = new Vector3(v.pos.Position.X, v.pos.Position.Y, v.pos.Position.Z);
                }
                else {
                    v.pos.Position = new Vector3(Core.Instance.camera2D.VisibleArea.Center.X + v.text.cameraSpaceOffset.X, -Core.Instance.camera2D.VisibleArea.Center.Y + v.text.cameraSpaceOffset.Y, v.pos.Position.Z);
                }
            }
        }
    }
}