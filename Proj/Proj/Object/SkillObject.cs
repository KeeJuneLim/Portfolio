using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj.Enum;
        
namespace Proj.Object {
    class SkillObject : FieldObject {
        private const string Idspace = Enum.Idspace.SkillObject;
        private double Radius;
        private string Shape;
        private double UpdateInterval;
        private double LifeTime;
        private double TimeSinceCreated;
        private double TimeSinceLastUpdated;

        public FieldObject User;

        public override void OnInitialize() {
            base.OnInitialize();

            Radius = DataProvider.Data.GetDouble(PropName.Radius);
            Shape = DataProvider.Data.GetString(PropName.Shape);
            UpdateInterval = DataProvider.Data.GetDouble(PropName.UpdateInterval);
            LifeTime = DataProvider.Data.GetDouble(PropName.LifeTime);
        }

        public override void OnUpdate(double dt) {
            base.OnUpdate(dt);
            
            TimeSinceCreated += dt;
            TimeSinceLastUpdated += dt;

            if (TimeSinceCreated >= LifeTime) {
                RemoveSkillObject();
            }

            if (TimeSinceLastUpdated >= UpdateInterval) {
                Hit();
                TimeSinceLastUpdated = 0;
            }
        }

        private void RemoveSkillObject() {
        }

        private void Hit() {

        }
    }
}
