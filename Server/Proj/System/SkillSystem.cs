using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Component;

namespace Server.EntitySystem {
    class SkillSystem : BaseSystem {
        public SkillSystem(FieldMap owner) : base(owner) { }
        public override void Initialize() {
            base.Initialize();

            RequireComponentTypes.Add(typeof(SkillInventoryComponent));
            RequireComponentTypes.Add(typeof(StatComponent));
            RequireComponentTypes.Add(typeof(MovementComponent));
        }

        public override void Update(double dt) {
            var requireComponentTypes = new List<Type>();


            //var entities = Owner.EntityManager.GetEntities(requireComponentTypes);
                // 로직 수행
        }

    }
}
