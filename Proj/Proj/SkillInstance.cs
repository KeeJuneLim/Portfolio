using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Proj.Enum;
using Proj.Provider;

namespace Proj {
    class SkillInstance {
        public FieldObject User;
        public const string Idspace = Enum.Idspace.Skill;
        public string ClassName;

        public DataProvider DataProvider = new();

        private double Cooldown;
        private double RemainCooldown;

        private string SkillObjectName;

        public bool IsSkillReady => RemainCooldown == 0;

        public SkillInstance(string className) {
            ClassName = className;
        }

        public void Initialize() {
            DataProvider.Initialize(ClassName);
            Cooldown = DataProvider.Data.GetDouble(PropName.BaseCooldown);
            SkillObjectName = DataProvider.Data.GetString(PropName.SkillObjectName);


        }

        public void Update(double dt) {
           CalculateCooldown(dt);
        }

        public void CreateSkillObject(Vector2 position) {
            RemainCooldown = Cooldown;

            FieldObjectFactory.Inst.CreateSkillObject(User, User.CurrentMap, ClassName, position);
        }

        private void CalculateCooldown(double dt) {
            if (RemainCooldown >= 0) {
                RemainCooldown -= dt;
            }

            if (RemainCooldown < 0) {
                RemainCooldown = 0;
            }
        }
    }
}
