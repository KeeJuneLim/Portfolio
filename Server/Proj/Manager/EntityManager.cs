using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Server.Component;
using Server.Enum;
using Server.ObjectEntity;

namespace Server.Manager {
    class EntityManager {
        public FieldMap Owner;
        public List<Entity> Entities = new();


        public void InitEntities(FieldMap owner) {
            Owner = owner;
        }

        public Entity CreateEntity(string idspace, FieldMap currentMap, Client client, string className, int level, Vector2 position) {
            var entity = new Entity();
            // TODO: need to init by data or db
            switch (idspace) {
                case Idspace.PC:
                    entity.AddComponent(new StatComponent {
                        Level = level,
                    });

                    entity.AddComponent(new MovementComponent {
                        CurrentMap = currentMap,
                        Position = position,

                    });

                    entity.AddComponent(new ClientComponent {
                        Client = client,
                        CurrentMap = currentMap
                    });

                    break;

                case Idspace.NPC:
                    entity.AddComponent(new StatComponent {
                        Level = level,
                    });

                    entity.AddComponent(new MovementComponent {
                        CurrentMap = currentMap,
                        Position = position,

                    });

                    break;

                case Idspace.SkillObject:
                    entity.AddComponent(new MovementComponent {
                        CurrentMap = currentMap,
                        Position = position,

                    });

                    break;
            }

            foreach (var comp in entity.Components) {
                comp.Value.Initialize();
                comp.Value.PropertyChanged += Owner.BufferedSync.OnPropertyChanged;
            }

            Entities.Add(entity);
            Owner.SystemManager.RegisterEntity(entity);
            Owner.ClientManager.RegisterEntity(entity);

            return entity;
        }

        public void RemoveEntity(int handle) {
            Entity removeEntity = null;
            foreach (var entity in Entities) {
                if (entity.Handle == handle) {
                    removeEntity = entity;
                    break;
                }
            }

            if (removeEntity != null) {
                Entities.Remove(removeEntity);
            }

            Owner.SystemManager.UnregisterEntity(handle);
            Owner.ClientManager.UnregisterEntity(handle);
        }
    }
}
