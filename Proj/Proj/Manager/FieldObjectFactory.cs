using System;
using System.Numerics;
using Proj.Component;
using Proj.Enum;
using Proj.Object;

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

        public FieldObject CreateSkillObject(FieldObject user, FieldMap currentMap, string className, Vector2 position) {
            var skillObject = new SkillObject() {
                User = user,
                ClassName = className,
                Position = position
            };

            skillObject.Components.Add(new MessageComponent());


            foreach (var component in skillObject.Components) {
                component.Owner = skillObject;
            }
            skillObject.CurrentMap = currentMap;

            return skillObject;
        }

        public FieldObject CreateFieldChar(string idspace, FieldMap currentMap, string className, int level, Vector2 position) {
            
            if (idspace != Idspace.PC && idspace != Idspace.NPC) {
                Console.WriteLine("Wrong Data - Object which Creating is not FieldChar");
                return null;
            }

            var fieldChar = new FieldChar() {
                Idspace = idspace,
                ClassName = className,
                Level = level,
                Position = position
            };


            switch (idspace) {
                case Idspace.PC:
                    fieldChar.Components.Add(new SkillInventoryComponent());
                    fieldChar.Components.Add(new MessageComponent());
                    break;
                case Idspace.NPC:
                    fieldChar.Components.Add(new SkillInventoryComponent());
                    fieldChar.Components.Add(new MessageComponent());
                    break;
            }

            foreach (var component in fieldChar.Components) {
                component.Owner = fieldChar;
            }

            fieldChar.OnInitialize();
            fieldChar.CurrentMap = currentMap;

            return fieldChar;
        }
    }
}
