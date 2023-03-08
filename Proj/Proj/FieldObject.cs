using System;
using System.Collections.Generic;
using System.Numerics;

namespace Proj {
    class FieldObject {
        public string Idspace;
        public string ClassName;

        public Vector2 Position;

        public List<BaseComponent> Components = new();

        public virtual void OnInitialize() {
           
        }

        public virtual void OnUpdate(double dt) {
          
        }
    }
}
