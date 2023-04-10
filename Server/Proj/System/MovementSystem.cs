using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Component;

namespace Server.EntitySystem {
    class MovementSystem : BaseSystem {
        public MovementSystem(FieldMap owner) : base(owner) { }

        public override void Initialize() {
            base.Initialize();

            RequireComponentTypes.Add(typeof(SkillInventoryComponent));
            RequireComponentTypes.Add(typeof(StatComponent));
            RequireComponentTypes.Add(typeof(MovementComponent));
        }

    }
}
