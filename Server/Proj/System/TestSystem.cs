using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Component;

namespace Server.EntitySystem {
    class TestSystem : BaseSystem{

        public TestSystem(FieldMap owner) : base(owner) { }
        public override void Initialize() {
            base.Initialize();

            //RequireComponentTypes.Add(typeof(SkillInventoryComponent));
            RequireComponentTypes.Add(typeof(StatComponent));
            RequireComponentTypes.Add(typeof(MovementComponent));
            RequireComponentTypes.Add(typeof(ClientComponent));
        }

        public override void Update(double dt) {
            base.Update(dt);

            foreach (var entity in Entities) {
                Console.WriteLine($"[{entity.Handle}] - TestSystem Updating");
            }
        }
    }
}
