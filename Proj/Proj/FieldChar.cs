
using System;

namespace Proj {
    class FieldChar : FieldObject {
        public int Level;

        public override void OnInitialize() {
            Console.WriteLine($"FieldChar Created - Name: {ClassName}, Level: {Level}, Position: {Position}");
        }

        public override void OnUpdate(double dt) {

        }
    }
}
