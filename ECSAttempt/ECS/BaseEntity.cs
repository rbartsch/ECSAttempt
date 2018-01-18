using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ECSAttempt.ECS {
    // Since every component must be associated with an entity, have a position for the entity
    // here which can be obtained from any component in their respective systems
    public abstract class BaseEntity {
        public string Name { get; private set; }
        public Guid Guid { get; private set; }
        public List<BaseComponent> Components { get; private set; }
        public GameTime GameTime { get; set; }
        internal bool destroyed = false;
        // Instantiation flag to know status of each entity and to avoid reinstantiation
        public bool instantiated = false;

        public BaseEntity(string name) {
            Name = name;
            Guid = Guid.NewGuid();
            Components = new List<BaseComponent>();
        }

        public void AddComponent(BaseComponent c) {
            // TODO: We want to check if a certain component exists that we don't one more than one of such as a Sprite component.
            Components.Add(c);
            c.RegisterToEntity(this);
            SystemManager.UpdateSystems(false);
        }

        public void RemoveComponent(BaseComponent c) {
            Components.Remove(c);
            c.DeregisterFromEntity();
            SystemManager.UpdateSystems(true);
        }

        public T GetComponent<T>() where T : BaseComponent {
            for (int i = 0; i < Components.Count; i++) {
                if (Components[i] is T) {
                    // return first instance
                    return Components[i] as T;
                }
            }

            return null;
        }

        public virtual void Initialize() { }

        public virtual void Update() { }
    }
}