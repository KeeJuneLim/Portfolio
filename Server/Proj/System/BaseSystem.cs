using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.ObjectEntity;

namespace Server.EntitySystem {
    class BaseSystem {
        public FieldMap Owner;
        public List<Entity> Entities = new();
        public List<Type> RequireComponentTypes = new();

        public BaseSystem(FieldMap owner) {
            Owner = owner;
        }

        public virtual void Initialize() {

        }

        public virtual void Update(double dt) {

        }

    }
}
