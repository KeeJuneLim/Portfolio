using System.Collections.Generic;

namespace Proj {

    class FieldObjectManager {
        private static FieldObjectManager instance;

        public static FieldObjectManager Inst {
            get {
                if (instance == null) {
                    instance = new FieldObjectManager();
                }

                return instance;
            }
        }


        private List<FieldObject> FieldObjects = new();
        public void OnInitialize() {
            foreach (var fieldObject in instance.FieldObjects) {
                fieldObject.OnInitialize();
            }

        }

        public void OnUpdate(double dt) {
            foreach (var fieldObject in instance.FieldObjects) {
                fieldObject.OnUpdate(dt);
            }
        }

        public void AddFieldObject(string idspace, string className) {
            var fieldObject = FieldObjectFactory.Inst.CreateFieldObject(idspace, className);
            FieldObjects.Add(fieldObject);
        }

    }
}
