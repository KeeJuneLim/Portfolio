using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Server.Enum;

namespace Server.Manager {
    class NPCGenerator {
        private FieldMap Owner;
        private Random RandomGenerator = new();
        public void InitGenerator(FieldMap owner) {
            Owner = owner;
            var generateInfo = Parser.GetCategory(Idspace.NPCGenerateInfo, Owner.ClassName);

            foreach (var npcInfo in generateInfo) {
                var npcName = npcInfo.GetString(PropName.NPCName);
                var level = npcInfo.GetInt(PropName.Level);
                var maxCount = npcInfo.GetInt(PropName.MaxCount);

                var position = npcInfo.GetVector2(PropName.Position);
                var positionOffset = npcInfo.GetDouble(PropName.PositionOffset);

                for (int i = 0; i < maxCount; ++i) {
                    var randX = (RandomGenerator.NextDouble() - 0.5) * positionOffset;
                    var randY = (RandomGenerator.NextDouble() - 0.5) * positionOffset;
                    var newPosition = position + new Vector2((float)randX, (float)randY);

                    var fieldObject = FieldObjectFactory.Inst.CreateFieldChar(Idspace.NPC, owner, null, npcName, level, newPosition);
                    Owner.FieldObjects.Add(fieldObject);
                }
            }
        }

        public void GenerateNpc() {

        }
    }
}
