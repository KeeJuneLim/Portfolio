using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Proj.Enum;

namespace Proj.Manager {
    class NPCGenerator {
        private FieldMap Owner;
        private Random RandomGenerator = new();
        public void InitGenerator(FieldMap owner) {
            Owner = owner;
            var generateInfo = Parser.GetCategory(Idspace.NPCGenerateInfo, Owner.ClassName);

            foreach (var npcInfo in generateInfo) {
                var npcName = npcInfo[PropName.NPCName];
                var level = int.Parse(npcInfo[PropName.Level]);
                var maxCount = int.Parse(npcInfo[PropName.MaxCount]);

                var positionStr = npcInfo[PropName.Position];
                string[] temp = positionStr.Substring(1, positionStr.Length - 2).Split(',');
                var position = new Vector2(float.Parse(temp[0]), float.Parse(temp[1]));
                var positionOffset = double.Parse(npcInfo[PropName.PositionOffset]);

                for (int i = 0; i < maxCount; ++i) {
                    var randX = (RandomGenerator.NextDouble() - 0.5) * positionOffset;
                    var randY = (RandomGenerator.NextDouble() - 0.5) * positionOffset;
                    var newPosition = position + new Vector2((float)randX, (float)randY);

                    var fieldObject = FieldObjectFactory.Inst.CreateFieldChar(Idspace.NPC, npcName, level, newPosition);
                    Owner.FieldObjects.Add(fieldObject);
                }
            }
        }

        public void GenerateNpc() {

        }
    }
}
