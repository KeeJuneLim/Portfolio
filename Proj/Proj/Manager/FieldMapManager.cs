using System.Collections.Generic;

namespace Proj {
    class FieldMapManager {
        private static FieldMapManager instance;
        private List<FieldMap> FieldMaps = new ();

        public static FieldMapManager Inst => instance ??= new FieldMapManager();

        public void OnInitialize() {
            FieldMaps.Add(new FieldMap());

            foreach (var fieldMap in instance.FieldMaps) {
                fieldMap.OnInitialize();
            }
        }

        public void OnUpdate(double dt) {
            foreach (var fieldMap in instance.FieldMaps) {
                fieldMap.OnUpdate(dt);
            }
        }
    }
}
