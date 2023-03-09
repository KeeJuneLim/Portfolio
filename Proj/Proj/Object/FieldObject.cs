using System;
using System.Collections.Generic;
using System.Numerics;
using Proj.Component;
using Proj.Enum;
using Proj.Message;
using Proj.Provider;

namespace Proj {
    class FieldObject {
        public string Idspace;
        public string ClassName;

        public FieldMap CurrentMap;
        public Vector2 Position;

        public List<BaseComponent> Components = new();
        public DataProvider DataProvider = new();

        public virtual void OnInitialize() {
            DataProvider.Initialize(ClassName);

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
    }
}
