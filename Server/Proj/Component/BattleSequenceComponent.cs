using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj.Enum;
using Proj.Message;

namespace Proj.Component {
    class BattleSequenceComponent : BaseComponent {


        public void ProcessBattleSequence(AttackMessage attackMessage) {
            var attackerAttackPower = attackMessage.AttackerAttackPower;
            var attackerHandle = attackMessage.AttackerHandle;

            var owner = Owner as FieldChar;
            var defenderDefense = owner.Defense;
            
            // calculations which need to be done when battle proceeds
            var finalDamage = attackerAttackPower - defenderDefense;

            owner.HP -= finalDamage;

            if (owner.HP <= 0) {
                //Dead
                var attacker = owner.CurrentMap.GetFieldObject(attackerHandle);
                
                // Rewards
                // exp, money ect
                //var rewardExp = owner.DataProvider.Data.GetInt(PropName.RewardExp);
                //var rewardMoney = owner.DataProvider.Data.GetInt(PropName.RewardMoney);
                var rewardExp = 0;
                var rewardMoney = 0;

                owner.SendMessage(attacker, MessageType.Killed, new KilledMessage {
                    KilledObjectHandle = owner.Handle,
                    RewardExp = rewardExp,
                    RewardMoney = rewardMoney
                });
            }
        }

       
    }
}
