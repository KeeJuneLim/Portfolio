using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Enum;
using Server.Message;

namespace Server.Component {
    class MessageComponent : BaseComponent {
        private Queue<MessageContainer> Messages = new();

        public override void Update(double dt) {
            base.Update(dt);

            HandleMessage();
        }

        public virtual void SendMessage(FieldObject receiver, string messageType, BaseMessage message) {
            receiver.ReceiveMessage(Owner, messageType, message);
        }

        public virtual void ReceiveMessage(FieldObject sender, string messageType, BaseMessage message) {
            //sender에 참조를 걸고 사용하면 매우 위험함
            //TODO: 이후 고유값 기능이 개발되면 FieldMap에서 sender를 직접 찾도록 수정해야 함
            Messages.Enqueue(new MessageContainer(sender, messageType, message));
        }

        private void HandleMessage() {
            if (Messages.Count <= 0) {
                return;
            }

            var msg = Messages.Dequeue();

            switch (msg.MessageType) {
                case MessageType.Attack:
                    // 공격
                    if (msg.Message is AttackMessage attackMessage) {
                        foreach (var comp in Owner.Components) {
                            if (comp is BattleSequenceComponent battle) {
                                battle.ProcessBattleSequence(attackMessage);
                            }
                        }
                    }

                    break;
                case MessageType.Heal:
                    // 로직
                    break;
                case MessageType.AddBuff:
                    // 버프 추가
                    break;
            }
        }
    }

    class MessageContainer {
        public MessageContainer(FieldObject sender, string messageType, BaseMessage message) {
            Sender = sender;
            MessageType = messageType;
            Message = message;
        }

        internal FieldObject Sender;
        internal string MessageType;
        internal BaseMessage Message;
    }
}
