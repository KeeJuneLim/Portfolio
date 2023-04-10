using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Component {
    class StatComponent : BaseComponent {
        private int _maxHP;
        public int MaxHP {
            get => _maxHP;
            set {
                if (_maxHP != value) {
                    _maxHP = value;
                    PropertyChanged?.Invoke(Owner.Handle, nameof(MaxHP), value.ToString());
                }
            }
        }

        private int _hp;
        public int HP {
            get => _hp;
            set {
                if (_hp != value) {
                    _hp = value;
                    PropertyChanged?.Invoke(Owner.Handle, nameof(HP), value.ToString());
                }
            }
        }

        public int Level;
        public int AttackPower;
        public int Defense;
        public int Exp;
    }
}
