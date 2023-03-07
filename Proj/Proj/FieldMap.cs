using System;
using System.Threading.Tasks;
using Proj.Enum;

namespace Proj {
    class FieldMap {


        public void OnInitialize() {
            // TODO: 필드맵에 어떤 NPC를 생성할 것인지의 데이터가 만들어지면 생성 로직을 여기에 적는다
            //var list = new List<string>();
            //foreach (var fos in list) {
            //    FieldObjectManager.Inst.AddFieldObject(fos);
            //}
            FieldObjectManager.Inst.AddFieldObject(Idspace.PC, "Barbarian"); // 임시 FieldObject1
            FieldObjectManager.Inst.AddFieldObject(Idspace.PC, "Sorceress"); // 임시 FieldObject2
            FieldObjectManager.Inst.AddFieldObject(Idspace.PC, "Assassin"); // 임시 FieldObject3

            FieldObjectManager.Inst.OnInitialize();
        }

        public void OnUpdate(double dt) {
            FieldObjectManager.Inst.OnUpdate(dt);
        }
    }
}
