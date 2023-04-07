using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.EntitySystem;
using Server.ObjectEntity;


namespace Server.Manager {
    class SystemManager {
        public List<BaseSystem> Systems = new();
        public FieldMap Owner;

        public void InitSystems(FieldMap owner) {
            Owner = owner;

            Systems.Add(new MovementSystem(owner)); // deals with things related to movements
            Systems.Add(new SkillSystem(owner)); // creates skills
            Systems.Add(new BattleSystem(owner)); // calculate and apply damage ect.
            Systems.Add(new TestSystem(owner)); // for test

            foreach (var system in Systems) {
                system.Initialize();
            }
        }

        public void UpdateSystems(double dt) {
            foreach (var system in Systems) {
                system.Update(dt);
            }
        }

        public void RegisterEntity(Entity entity) {
            foreach (var system in Systems) {
                var requirementCompleted = true;
                foreach (var requireType in system.RequireComponentTypes) {
                    if (entity.HasComponent(requireType) == false) {
                        requirementCompleted = false;
                        break;
                    }
                }

                if (requirementCompleted) {
                    system.Entities.Add(entity);
                }
            }
        }

        public void UnregisterEntity(int handle) {
            foreach (var system in Systems) {
                for (int i = 0; i < system.Entities.Count; ++i) {
                    if (system.Entities[i].Handle == handle) {
                        system.Entities.Remove(system.Entities[i]);
                        break;
                    }
                }
            }
        }
    }
}
