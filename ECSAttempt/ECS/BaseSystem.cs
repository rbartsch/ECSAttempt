using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSAttempt.ECS
{
    public abstract class BaseSystem
    {
        public abstract void SyncInterestedEntities(bool remove);

        protected BaseEntity GetEntity<T>( Dictionary<BaseEntity, T> entities) {
            if (EntityManager.entities.Count > 0) {
                BaseEntity e = EntityManager.entities.Last(item => item is BaseEntity);
                if (!entities.ContainsKey(e)) {
                    return e;
                }
                else {
                    return null;
                }
            }
            else {
                return null;
            }
        }

        protected T GetComponent<T>() where T: BaseComponent {
            BaseEntity e = EntityManager.entities.Last(item => item is BaseEntity);
            return ConvertComponent<T>(e.Components.Find(item => item is T));
        }

        protected bool LatestEntityHasComponent<T>() where T: BaseComponent {
            if (EntityManager.entities.Count > 0) {
                BaseEntity e = EntityManager.entities.Last(item => item is BaseEntity);
                if(e.Components.Contains(ConvertComponent<T>(e.Components.Find(item => item is T)))) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        protected bool FindComponent<T>(BaseEntity entity) where T: BaseComponent {
            if(entity.Components.Count <= 0) {
                return false;
            }
            else if(!entity.Components.Contains(ConvertComponent<T>(entity.Components.Find(item => item is T)))){
                return true;
            }
            else {
                return false;
            }
        }

        protected List<BaseEntity> GetListOfRemovableEntitiesOf<T>() where T: BaseComponent {
            List<BaseEntity> entities = new List<BaseEntity>();
            if(EntityManager.entities.Count > 0) {
                entities = EntityManager.entities.FindAll(FindComponent<T>);
            }

            return entities;
        }

        protected void RemoveEntities<T>(Dictionary<BaseEntity, T> entities, List<BaseEntity> removables) {
            List<BaseEntity> entitiesAlreadyDestroyed = entities.Keys.ToList();
            for (int i = 0; i < entitiesAlreadyDestroyed.Count; i++) {
                if (entitiesAlreadyDestroyed[i].destroyed) {
                    if (entities.ContainsKey(entitiesAlreadyDestroyed[i])) {
                        entities.Remove(entitiesAlreadyDestroyed[i]);
                    }
                }
            }

            for (int i = 0; i < removables.Count; i++) {
                if (entities.ContainsKey(removables[i])) {
                    entities.Remove(removables[i]);
                }
            }
        }

        protected List<BaseEntity> GetListOfDestroyedEntities(List<BaseEntity> entities) {
            List<BaseEntity> entityKeysToRemove = new List<BaseEntity>();
            for (int i = 0; i < entities.Count; i++) {
                if (entities[i].destroyed) {
                    entityKeysToRemove.Add(entities[i]);
                }
            }

            return entityKeysToRemove;
        }

        protected T ConvertComponent<T>(BaseComponent c) where T: BaseComponent {
            return (T)c;
        }

        //public bool CheckDuplicate(List<BaseEntity> entityKeys, BaseEntity entity) {
        //    bool duplicate = false;
        //    foreach (BaseEntity e in entityKeys) {
        //        if (e == entity)
        //            duplicate = true;
        //    }

        //    return duplicate;
        //}
    }
}
