
using System;
using Proj.Enum;
using Proj.Message;

namespace Proj {
    class FieldChar : FieldObject {
        public int MaxHP;
        public int HP;
        public int AttackPower;
        public int Defense;
        public int Level;

        public override void OnInitialize() {
            base.OnInitialize();
        
            InitProperties();
        }

        public override void OnUpdate(double dt) {
            base.OnUpdate(dt);

        }

        private void InitProperties() {
            MaxHP = DataProvider.Data.GetInt(PropName.BaseHP);
            HP = MaxHP;
            AttackPower = DataProvider.Data.GetInt(PropName.BaseAttackPower);
            Defense = DataProvider.Data.GetInt(PropName.BaseDefense);

        }
    }
}
