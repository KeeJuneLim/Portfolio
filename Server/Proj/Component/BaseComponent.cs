using System;

namespace Server {
    class BaseComponent {
        public FieldObject Owner;

        public virtual void Initialize() {

        }

        public virtual void Update(double dt) {
        }
    }
}
