using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Server.Enum;
using Server.Message;
using Server.Manager;
using Server.ObjectEntity;

namespace Server {
    class FieldMap {
        public string ClassName {
            get;
            set;
        }
        public int ClassId;

        public NPCGenerator NpcGenerator = new();

        public EntityManager EntityManager = new();
        public SystemManager SystemManager = new();
        public ClientManager ClientManager = new();
        public BufferedSynchronization BufferedSync = new();


        public List<int> RemoveRequestedEntityHandle = new();

        public void OnInitialize() {
            // 초기 NPC 생성 (풀젠)
            EntityManager.InitEntities(this);
            SystemManager.InitSystems(this);
            ClientManager.InitClientManager(this);
            BufferedSync.InitBufferedSync(this);

            NpcGenerator.InitGenerator(this);
        }

        public void OnUpdate(double dt) {
            SystemManager.UpdateSystems(dt);
            BufferedSync.SendSyncData();
            BufferedSync.BroadcastSyncData();
        }

        public void RemoveEntity(int handle) {
            RemoveRequestedEntityHandle.Add(handle);
        }

        public void AddPlayerCharacter(Client client, DBPlayerInfo info) {
            var jobName = info.JobName;
            var level = info.Level;
            var position = info.Position;

            var entity = EntityManager.CreateEntity(Idspace.PC, this, client, jobName, level, position);
        }


        //public FieldObject GetFieldObject(int handle) {
        //    foreach (var fieldObject in FieldObjects) {
        //        if (fieldObject.Handle == handle) {
        //            return fieldObject;
        //        }
        //    }

        //    return null;
        //}

        //public FieldChar GetFieldChar(int handle) {
        //    foreach (var fieldObject in FieldObjects) {
        //        if (fieldObject is FieldChar fieldChar) {
        //            if (fieldChar.Handle == handle) {
        //                return fieldChar;
        //            }
        //        }
        //    }

        //    return null;
        //}

        //public List<FieldChar> GetPlayerList() {
        //    var playerList = new List<FieldChar>();
        //    foreach (var fieldObject in FieldObjects) {
        //        if (fieldObject.Idspace != Idspace.PC) {
        //            continue;
        //        }

        //        playerList.Add((FieldChar)fieldObject);
        //    }

        //    return playerList;
        //}

        // TODO: 1.need to improve performance / 2.find listed fo by handle
        //public List<FieldObject> GetCollidedFieldObjects(FieldObject self) {
        //    var collidedFieldObjects = new List<FieldObject>();

        //    foreach (var fieldObject in FieldObjects) {
        //        if (self.IsCollided(fieldObject)) {
        //            collidedFieldObjects.Add(fieldObject);
        //        }
        //    }

        //    return collidedFieldObjects;
        //}

        //public List<FieldChar> GetCollidedFieldChars(FieldObject self) {
        //    var collidedFieldChar = new List<FieldChar>();

        //    foreach (var fieldObject in FieldObjects) {
        //        if (self.IsCollided(fieldObject)) {
        //            if (fieldObject is FieldChar fieldChar) {
        //                collidedFieldChar.Add(fieldChar);
        //            }
        //        }
        //    }

        //    return collidedFieldChar;
        //}
    }
}
