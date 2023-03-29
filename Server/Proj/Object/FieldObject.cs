using System;
using System.Collections.Generic;
using System.Numerics;
using Server.Component;
using Server.Enum;
using Server.Manager;
using Server.Message;
using Server.Provider;

namespace Server {
    class FieldObject {
        public string Idspace;
        public string ClassName;

        public double Radius;

        public FieldMap CurrentMap;
        public Vector2 Position;

        public List<BaseComponent> Components = new();
        public DataProvider DataProvider = new();

        public int Handle;

        public virtual void OnInitialize() {
            Handle = HandleManager.Inst.CreateHandle();
            DataProvider.Initialize(ClassName);

            Radius = DataProvider.Data.GetDouble(PropName.Radius);
            foreach (var component in Components) {
                component.Initialize();
            }
        }

        public virtual void OnUpdate(double dt) {
            foreach (var component in Components) {
                component.Update(dt);
            }
        }

        public virtual void SendMessage(FieldObject receiver, string messageType, BaseMessage message) {
            foreach (var comp in Components) {
                if (comp is MessageComponent messageComponent) {
                    messageComponent.SendMessage(receiver, messageType, message);
                    return;
                }
            }
        }

        public virtual void ReceiveMessage(FieldObject sender, string messageType, BaseMessage message) {
            foreach (var comp in Components) {
                if (comp is MessageComponent messageComponent) {
                    messageComponent.ReceiveMessage(sender, messageType, message);
                    return;
                }
            }
        }

        public bool IsInRange(Vector2 targetPosition, double range) {
            var distance = (Position - targetPosition).Length();

            if (distance <= Radius + range) {
                return true;
            }

            return false;
        }

        public bool IsCollided(FieldObject target) {
            if (IsInRange(target.Position, target.Radius)) {
                return true;
            }

            return false;
        }

    }
}
