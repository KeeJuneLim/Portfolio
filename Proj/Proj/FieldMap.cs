using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proj.Enum;
using Proj.Manager;
using Proj.Message;

namespace Proj {
    class FieldMap {
        public string ClassName {
            get;
            set;
        }
        public int ClassId;

        public NPCGenerator NpcGenerator;
        public List<FieldObject> FieldObjects;

        public void OnInitialize() {
            if (NpcGenerator == null) {
                NpcGenerator = new NPCGenerator();
            }

            if (FieldObjects == null) {
                FieldObjects = new List<FieldObject>();
            }

            // 초기 NPC 생성 (풀젠)
            NpcGenerator.InitGenerator(this);
            InitFieldObjects();


            foreach (var fo in FieldObjects) {
                var fc = (FieldChar)fo;
                fc.SendMessage(fc, MessageType.Attack, new AttackMessage {
                    AttackerAttackPower = fc.DataProvider.Data.GetInt(PropName.BaseAttackPower),
                    DefenderDefense = fc.DataProvider.Data.GetInt(PropName.BaseDefense)
                });
            }


        }

        public void OnUpdate(double dt) {
            UpdateFieldObjects(dt);
        }

        public void InitFieldObjects() {
            //foreach (var fieldObject in FieldObjects) {
            //    fieldObject.OnInitialize();
            //}
        }

        public void UpdateFieldObjects(double dt) {
            foreach (var fieldObject in FieldObjects) {
                fieldObject.OnUpdate(dt);
            }
        }

        public void RemoveFieldObject() {

        }
    }
}
