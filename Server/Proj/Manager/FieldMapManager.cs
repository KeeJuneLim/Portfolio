using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Enum;

namespace Server.Manager {
    class FieldMapManager {
        private static FieldMapManager instance;
        private List<FieldMap> FieldMaps = new ();

        public static FieldMapManager Inst => instance ??= new FieldMapManager();

        public void OnInitialize() {
            // Field와 Dungeon은 다른 방식으로 제작할 예정
            // 일단 Field를 먼저 만들고 Dungeon은 추후 제작
            //TODO: Dungeon 로직을 만들어야 함
            var fieldInfo = Parser.GetCategory(Idspace.Map, Category.Field);

            foreach (var field in fieldInfo) {
                FieldMaps.Add(new FieldMap {
                    ClassName = field.GetString(PropName.ClassName),
                    ClassId = field.GetInt(PropName.ClassId)
                });
            }

            foreach (var fieldMap in instance.FieldMaps) {
                fieldMap.OnInitialize();
            }
        }

        public void OnUpdate(double dt) {
            //TODO: 성능 비교 해봐야 함
            //foreach (var fieldMap in instance.FieldMaps) {
            //    fieldMap.OnUpdate(dt);
            //}

            Parallel.ForEach(FieldMaps, fieldMap => {
                fieldMap.OnUpdate(dt);
            });
        }
    }
}
