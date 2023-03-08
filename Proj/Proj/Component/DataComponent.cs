﻿using System;
using System.Collections.Generic;

namespace Proj.Component {
    class DataComponent : BaseComponent {
        public Dictionary<string, string> Data = new();

        public override void OnInitialize() {
            base.OnInitialize();
            Data = Parser.GetClass(Owner.ClassName);
        }

        public override void OnUpdate(double dt) {
            base.OnUpdate(dt);

        }
    }
}
