using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ECSAttempt.ECS.Components;

namespace ECSAttempt.ECS.Systems {
    public class Line2DSystem : BaseSystem, IDisposable {
        public Dictionary<BaseEntity, ValueTuple<Line2DComponent, PositionComponent>> entities;

        public Line2DSystem() {
            entities = new Dictionary<BaseEntity, ValueTuple<Line2DComponent, PositionComponent>>();
            SystemManager.Add(this);
        }

        public override void SyncInterestedEntities(bool remove) {
            if (LatestEntityHasComponent<Line2DComponent>() && LatestEntityHasComponent<PositionComponent>()) {
                BaseEntity e = GetEntity(entities);
                if (e != null) {
                    entities.Add(e, (GetComponent<Line2DComponent>(), GetComponent<PositionComponent>()));
                }
            }

            if (remove) {
                List<BaseEntity> entitiesRemovable = GetListOfRemovableEntitiesOf<Line2DComponent>();
                entitiesRemovable.Union(GetListOfRemovableEntitiesOf<PositionComponent>());
                RemoveEntities(entities, entitiesRemovable);
            }

            Initialize();
        }

        public void Initialize() {
            foreach (KeyValuePair<BaseEntity, ValueTuple<Line2DComponent, PositionComponent>> p in entities) {
                if (!p.Key.instantiated) {
                    var v = (line: p.Value.Item1, pos: p.Value.Item2);

                    if (v.line != null && v.line.Texture2D == null) {
                        v.line.Texture2D = new Texture2D(Core.Instance.GraphicsDevice, 1, 1);
                        v.line.Texture2D.SetData(new[] { v.line.Color });
                        v.line.Edge = v.line.EndPosition - new Vector2(v.pos.Position.X, v.pos.Position.Y);
                        v.line.Angle = (float)Math.Atan2(-v.line.Edge.Y, v.line.Edge.X);
                    }
                }
            }
        }

        public void Draw() {
            foreach (KeyValuePair<BaseEntity, ValueTuple<Line2DComponent, PositionComponent>> p in entities) {
                var v = (line: p.Value.Item1, pos: p.Value.Item2);

                if (v.line != null && v.line.Texture2D != null) {
                    Core.Instance.spriteBatch.Draw(v.line.Texture2D, new Rectangle((int)v.pos.Position.X, -(int)v.pos.Position.Y, (int)v.line.Edge.Length(), v.line.Width), null, v.line.Color, v.line.Angle, new Vector2(0, 0), SpriteEffects.None,v.line.LayerDepth);
                }
            }
        }

        public void Dispose() {
            foreach (KeyValuePair<BaseEntity, ValueTuple<Line2DComponent, PositionComponent>> p in entities) {
                var v = (line: p.Value.Item1, pos: p.Value.Item2);
                if (v.line != null) {
                    v.line.Texture2D.Dispose();
                }
            }
        }
    }
}
