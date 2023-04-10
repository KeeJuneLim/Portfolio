using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Server.Component {
    class MovementComponent : BaseComponent {
        public FieldMap CurrentMap;
        public Vector2 Position;
        public Vector2 RequestedPosition;
        public double MoveSpeed;
        public Vector2 LookDirection;
    }
}
