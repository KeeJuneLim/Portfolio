
using System;
using Server.Message;
using Server.Enum;

namespace Server {
    class FieldChar : FieldObject {
        public Client Client;
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
