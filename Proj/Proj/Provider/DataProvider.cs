using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Provider {
    class DataProvider {
        public ClassData Data;
        public void Initialize(string className) {
            if (Data == null) {
                Data = new ClassData(className);
            }
        }
    }
}
