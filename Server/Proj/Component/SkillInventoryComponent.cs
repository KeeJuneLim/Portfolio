using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Enum;

namespace Server.Component {
    class SkillInventoryComponent : BaseComponent {
        private List<SkillInstance> SkillList = new ();
        public override void Initialize() {
            base.Initialize();

            var skillNameArr = Owner.DataProvider.Data.GetStringArray(PropName.SkillList);
            foreach (var skill in skillNameArr) {
                var skillInstance = new SkillInstance(skill);
                skillInstance.Initialize();
                skillInstance.User = Owner;
                SkillList.Add(skillInstance);
            }
        }

        public override void Update(double dt) {
            base.Update(dt);

            foreach (var skillInst in SkillList) {
                skillInst.Update(dt);
            }
        }
    }
}
