using System;

namespace Proj {
    class BaseComponent {
        public FieldObject Owner;

        public virtual void Initialize() {

        }

        public virtual void Update(double dt) {
        }
    }
}
