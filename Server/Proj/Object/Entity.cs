using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Component;
using Server.Manager;
using Server.Provider;

namespace Server.ObjectEntity {
    class Entity {
        public int Handle;
        public Dictionary<Type, BaseComponent> Components = new();

        public DataProvider DataProvider = new();


        public Entity() {
            var handle = HandleManager.Inst.CreateHandle();
            Handle = handle;
        }

        public void AddComponent<T>(T component) where T : BaseComponent {
            Components[typeof(T)] = component;
            component.Owner = this;
            component.Initialize();
        }

        public T GetComponent<T>() where T : BaseComponent {
            if (Components.TryGetValue(typeof(T), out var component)) {
                return component as T;
            }

            return null;
        }

        public bool HasComponent(Type type) {
            return Components.TryGetValue(type, out _);
        }
    }
}
