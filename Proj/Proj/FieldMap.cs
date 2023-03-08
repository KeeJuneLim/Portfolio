using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proj.Enum;
using Proj.Manager;

namespace Proj {
    class FieldMap {
        public string ClassName;
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
        }

        public void OnUpdate(double dt) {
            UpdateFieldObjects(dt);
        }

        public void InitFieldObjects() {
            foreach (var fieldObject in FieldObjects) {
                fieldObject.OnInitialize();
            }
        }

        public void UpdateFieldObjects(double dt) {
            foreach (var fieldObject in FieldObjects) {
                fieldObject.OnUpdate(dt);
            }
        }
    }
}
