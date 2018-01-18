using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ECSAttempt.ECS {
    public class EntityManager {
        public static List<BaseEntity> entities;

        public EntityManager() {
            entities = new List<BaseEntity>();
        }

        internal static void Add(BaseEntity entity) {
            entities.Add(entity);
        }

        internal static void Remove(BaseEntity entity) {
            entities.Remove(entity);
        }

        // TODO: Have a "BatchInstantiate" where you don't do SystemManager.UpdateSystem per spawn
        // but process after a batch just once.
        public static void Instantiate(BaseEntity entity) {
            Add(entity);
            SystemManager.UpdateSystems(false);
            entity.instantiated = true;
        }

        public static void Destroy(BaseEntity entity) {
            Remove(entity);
            entity.destroyed = true;
            SystemManager.UpdateSystems(true);
        }

        public void Initialize() {
            for (int i = 0; i < entities.Count; i++) {
                entities[i].Initialize();
            }
        }

        public void Update(GameTime gameTime) {
            for (int i = 0; i < entities.Count; i++) {
                entities[i].GameTime = gameTime;
                entities[i].Update();
            }
        }
    }
}