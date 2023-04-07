using System;
using Server.ObjectEntity;

namespace Server.Component {
    class BaseComponent {
        public Entity Owner;
        public Action<int, string, string> PropertyChanged;

        public virtual void Initialize() {

        }

        public virtual void Update(double dt) {
        }
    }
}
