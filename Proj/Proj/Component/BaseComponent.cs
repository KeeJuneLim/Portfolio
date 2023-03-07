using System;

namespace Proj {
    class BaseComponent {
        public FieldObject Owner;

        public virtual void OnInitialize() {

        }

        public virtual void OnUpdate(double dt) {
            Console.WriteLine($"{Owner.ClassName}'s Compoent 'DataComp' Updated");
        }
    }
}
