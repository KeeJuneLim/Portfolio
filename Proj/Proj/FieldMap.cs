using System;
using System.Collections.Generic;
using System.Numerics;
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

        public NPCGenerator NpcGenerator = new();

        public List<FieldObject> FieldObjects = new();

        public List<int> RemoveRequestedFieldObjectHandle = new();

        public void OnInitialize() {
            // 초기 NPC 생성 (풀젠)
            NpcGenerator.InitGenerator(this);

        }

        public void OnUpdate(double dt) {
            UpdateFieldObjects(dt);
            ExecuteRemoval();
        }

        public void UpdateFieldObjects(double dt) {
            foreach (var fieldObject in FieldObjects) {
                fieldObject.OnUpdate(dt);
            }
        }

        public void RemoveFieldObject(int handle) {
            RemoveRequestedFieldObjectHandle.Add(handle);
        }

        private void ExecuteRemoval() {
            if (RemoveRequestedFieldObjectHandle.Count <= 0) {
                return;
            }
            foreach (var handle in RemoveRequestedFieldObjectHandle) {
                for (int i = 0; i < FieldObjects.Count; ++i) {
                    if (FieldObjects[i].Handle == handle) {
                        FieldObjects[i] = null;
                        FieldObjects.Remove(FieldObjects[i]);
                        break;
                    }
                }
            }
        }

        public FieldObject GetFieldObject(int handle) {
            foreach (var fieldObject in FieldObjects) {
                if (fieldObject.Handle == handle) {
                    return fieldObject;
                }
            }

            return null;
        }

        public FieldChar GetFieldChar(int handle) {
            foreach (var fieldObject in FieldObjects) {
                if (fieldObject is FieldChar fieldChar) {
                    if (fieldChar.Handle == handle) {
                        return fieldChar;
                    }
                }
            }

            return null;
        }

        // TODO: 1.need to improve performance / 2.find listed fo by handle
        public List<FieldObject> GetCollidedFieldObjects(FieldObject self) {
            var collidedFieldObjects = new List<FieldObject>();

            foreach (var fieldObject in FieldObjects) {
                if (self.IsCollided(fieldObject)) {
                    collidedFieldObjects.Add(fieldObject);
                }
            }

            return collidedFieldObjects;
        }

        public List<FieldChar> GetCollidedFieldChars(FieldObject self) {
            var collidedFieldChar = new List<FieldChar>();

            foreach (var fieldObject in FieldObjects) {
                if (self.IsCollided(fieldObject)) {
                    if (fieldObject is FieldChar fieldChar) {
                        collidedFieldChar.Add(fieldChar);
                    }
                }
            }

            return collidedFieldChar;
        }
    }
}
