using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Component;
using Server.ObjectEntity;

namespace Server.Manager {
    class ClientManager {
        public FieldMap Owner;
        private List<Entity> Entities = new();

        public void InitClientManager(FieldMap fieldMap) {
            Owner = fieldMap;
        }

        public List<Entity> GetClientEntities() {
            return Entities;
        }

        public void RegisterEntity(Entity entity) {
            if (entity.HasComponent(typeof(ClientComponent))) {
                Entities.Add(entity);
            }
        }

        public void UnregisterEntity(int handle) {
            for (int i = 0; i < Entities.Count; ++i) {
                if (Entities[i].Handle == handle) {
                    Entities.Remove(Entities[i]);
                    break;
                }
            }
        }
    }
}
