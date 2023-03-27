using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj.Enum;
using Proj.Message;

namespace Proj.Object {
    class SkillObject : FieldObject {
        private const string Idspace = Enum.Idspace.SkillObject;
        private string Shape;
        private double UpdateInterval;
        private double LifeTime;
        private double TimeSinceCreated;
        private double TimeSinceLastUpdated;

        public FieldObject User;

        public override void OnInitialize() {
            base.OnInitialize();

            Shape = DataProvider.Data.GetString(PropName.Shape);
            UpdateInterval = DataProvider.Data.GetDouble(PropName.UpdateInterval);
            LifeTime = DataProvider.Data.GetDouble(PropName.LifeTime);
        }

        public override void OnUpdate(double dt) {
            base.OnUpdate(dt);
            
            TimeSinceCreated += dt;
            TimeSinceLastUpdated += dt;

            if (TimeSinceCreated >= LifeTime) {
                RemoveSkillObject();
            }

            if (TimeSinceLastUpdated >= UpdateInterval) {
                Hit();
                TimeSinceLastUpdated = 0;
            }
        }

        private void RemoveSkillObject() {
            User.CurrentMap.RemoveFieldObject(Handle);
        }

        private void Hit() {
            // User가 FieldChar가 아니라면 잘못된 값이 들어온 것이므로 처리하지 않는다
            if (User is FieldChar user) {
                var collidedFieldChar = user.CurrentMap.GetCollidedFieldChars(user);

                foreach (var fieldChar in collidedFieldChar) {
                    user.SendMessage(fieldChar, MessageType.Attack, new AttackMessage {
                        AttackerHandle = user.Handle,
                        AttackerAttackPower = user.AttackPower,
                        // 앞으로 연산식에 필요한 정보를 이곳에 추가
                    });
                }

            }
        }
    }
}
