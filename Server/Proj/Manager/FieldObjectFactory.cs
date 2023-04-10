using System;
using System.Numerics;
using Server.Component;
using Server.Enum;
using Server.Object;

namespace Server.Manager {
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

        //public FieldObject CreateSkillObject(FieldObject user, FieldMap currentMap, string className, Vector2 position) {
        //    var skillObject = new SkillObject() {
        //        User = user,
        //        ClassName = className,
        //        Position = position
        //    };

        //    skillObject.Components.Add(new MessageComponent());


        //    foreach (var component in skillObject.Components) {
        //        component.Owner = skillObject;
        //    }
        //    skillObject.CurrentMap = currentMap;

        //    return skillObject;
        //}

       
    }
}
