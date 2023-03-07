using Proj.Component;
using Proj.Enum;

namespace Proj {
    class FieldObjectFactory {

        private static FieldObjectFactory instance;

        public static FieldObjectFactory Inst {
            get {
                if (instance == null) {
                    instance = new FieldObjectFactory();
                }

                return instance;
            }
        }

        public FieldObject CreateFieldObject(string idspace, string className) {
            var fieldObject = new FieldObject {
                ClassName = className,
                Idspace = idspace
        };
           

            switch (idspace) {
                case Idspace.PC:
                    fieldObject.Components.Add(new DataComponent());
                    break;
                case Idspace.NPC:
                    fieldObject.Components.Add(new DataComponent());
                    break;
                case Idspace.Skill:
                    break;
            }

            foreach (var component in fieldObject.Components) {
                component.Owner = fieldObject;
            }

            return fieldObject;
        }
    }
}
