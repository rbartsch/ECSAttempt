using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECSAttempt.Logger;

namespace ECSAttempt.ECS {
    public abstract class BaseComponent {
        public Guid Guid { get; set; }
        // A component is always attached to an entity, get the entity that the component lives on
        public BaseEntity Entity { get; private set; }

        public BaseComponent() {
            Guid = Guid.NewGuid();
        }

        public void RegisterToEntity(BaseEntity attachedEntity) {
            // Only registerable once
            if (Entity == null) {
                Entity = attachedEntity;
            }
            else {
                Utils.Log("An entity is only registerable once to a component and only after the component has been added to the entity's active list of components.", LogType.Warning);
            }
        }

        public void DeregisterFromEntity() {
            if (Entity != null) {
                Entity = null;
            }
            else {
                Utils.Log("An entity is only deregisterable once from a component and only after the component has been removed from the entity's active list of components.", LogType.Warning);
            }
        }
    }
}