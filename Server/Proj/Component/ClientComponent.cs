using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packet;

namespace Server.Component {
    class ClientComponent : BaseComponent {
        public Client Client;
        public FieldMap CurrentMap;

        public ClientComponent() {
        }

        public override void Initialize() {
            base.Initialize();
            Client.Component = this;
        }

        public void SendPacket(PKS_BASE pks) {
            Client.Send(pks);
        }
    }
}
