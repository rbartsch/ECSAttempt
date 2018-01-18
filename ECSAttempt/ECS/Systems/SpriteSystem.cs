using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ECSAttempt.ECS.Components;

namespace ECSAttempt.ECS.Systems {
    public class SpriteSystem : BaseSystem {
        public Dictionary<BaseEntity, ValueTuple<SpriteComponent, PositionComponent>> entities;

        public SpriteSystem() {
            entities = new Dictionary<BaseEntity, ValueTuple<SpriteComponent, PositionComponent>>();
            SystemManager.Add(this);
        }

        (SpriteComponent sprite, PositionComponent pos) GetComponentsAsTuple(SpriteComponent s, PositionComponent p) {
            var sprite = s;
            var pos = p;
            return (sprite, pos);
        }

        public override void SyncInterestedEntities(bool remove) {
            if (LatestEntityHasComponent<SpriteComponent>() && LatestEntityHasComponent<PositionComponent>()) {
                BaseEntity e = GetEntity(entities);
                if (e != null) {
                    entities.Add(e, (GetComponent<SpriteComponent>(), GetComponent<PositionComponent>()));
                }
            }

            if (remove) {                
                List<BaseEntity> entitiesRemovable = GetListOfRemovableEntitiesOf<SpriteComponent>();
                entitiesRemovable.Union(GetListOfRemovableEntitiesOf<PositionComponent>());
                RemoveEntities(entities, entitiesRemovable);
            }

            Load();
        }

        public void Load() {
            foreach (KeyValuePair<BaseEntity, ValueTuple<SpriteComponent, PositionComponent>> p in entities) {
                if (!p.Key.instantiated) {
                    //var v = (sprite: p.Value.Item1, pos: p.Value.Item2);
                    var v = GetComponentsAsTuple(p.Value.Item1, p.Value.Item2);

                    v.sprite.Texture2D = Core.Instance.Content.Load<Texture2D>(v.sprite.Texture2DPath);
                    v.sprite.Origin = new Vector2(v.sprite.Texture2D.Bounds.Center.X, v.sprite.Texture2D.Bounds.Center.Y);
                }
            }
        }

        public void Draw() {
            foreach(KeyValuePair<BaseEntity, ValueTuple<SpriteComponent, PositionComponent>> p in entities) {
                //var v = (sprite: p.Value.Item1, pos: p.Value.Item2);
                var v = GetComponentsAsTuple(p.Value.Item1, p.Value.Item2);

                if (v.sprite.Texture2D != null) {
                    Core.Instance.spriteBatch.Draw(v.sprite.Texture2D, new Vector2(v.pos.Position.X, -v.pos.Position.Y), null, v.sprite.Color, v.sprite.Rotation, v.sprite.Origin, v.sprite.Scale, SpriteEffects.None, v.sprite.LayerDepth);
                }
            }
        }
    }
}