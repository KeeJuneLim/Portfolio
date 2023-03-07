using System;
using System.Collections.Generic;

namespace Proj {
    class FieldObject {
        public string Idspace;
        public string ClassName;

        public List<BaseComponent> Components = new();

        public virtual void OnInitialize() {
            Console.WriteLine($"{ClassName} - Initialized");
            foreach (var component in Components) {
                component.OnInitialize();
            }
        }

        public virtual void OnUpdate(double dt) {
            Console.WriteLine($"{ClassName} - Updated");
            foreach (var component in Components) {
                component.OnUpdate(dt);
            }
        }
    }
}
