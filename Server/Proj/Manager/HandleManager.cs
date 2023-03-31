using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Manager {
    class HandleManager {
        private static int handleIndex = 1;
        private static HandleManager instance;

        private static object _lock = new();
        private static object _lock2 = new();

        public static HandleManager Inst {
            get {
                if (instance == null) {
                    instance = new HandleManager();
                }

                return instance;
            }
        }

        public int CreateHandle() {
            lock (_lock) {
                var createdHandle = handleIndex;
                ++handleIndex;

                return createdHandle;
            }
        }
    }
}
