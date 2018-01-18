using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSAttempt.ECS {
    public class SystemManager {
        internal static List<BaseSystem> systems;

        public SystemManager() {
            systems = new List<BaseSystem>();
        }

        internal static void Add(BaseSystem system) {
            systems.Add(system);
        }

        internal static void Remove(BaseSystem system) {
            systems.Remove(system);
        }

        internal static void UpdateSystems(bool remove) {
            for (int i = 0; i < systems.Count; i++) {
                systems[i].SyncInterestedEntities(remove);
            }
        }
    }
}