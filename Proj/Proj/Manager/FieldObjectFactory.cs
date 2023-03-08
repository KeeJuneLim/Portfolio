using System;
using System.Numerics;
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

        public FieldObject CreateFieldObject(string idspace, string className, int level, Vector2 position) {
            var fieldObject = new FieldObject() {
                Idspace = idspace,
                ClassName = className,
                Position = position
            };
           

            switch (idspace) {
                case Idspace.Skill:
                    break;
            }

            foreach (var component in fieldObject.Components) {
                component.Owner = fieldObject;
            }

            return fieldObject;
        }

        public FieldObject CreateFieldChar(string idspace, string className, int level, Vector2 position) {
            
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
                    fieldChar.Components.Add(new DataComponent());
                    fieldChar.Components.Add(new SkillInventoryComponent());
                    break;
                case Idspace.NPC:
                    fieldChar.Components.Add(new DataComponent());
                    fieldChar.Components.Add(new SkillInventoryComponent());
                    break;
         
            }

            foreach (var component in fieldChar.Components) {
                component.Owner = fieldChar;
            }

            return fieldChar;
        }
    }
}
