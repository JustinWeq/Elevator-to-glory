using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Buffs
{
    class Stun : Buff
    {
        protected override void BuffEffect()
        {
            //make sure the owner cannot move
            if(on_enemy)
            {
                if (enemy_handle == null)
                    return;
                enemy_handle.SetCanMove(false);
                enemy_handle.SetCanCast(false);
                enemy_handle.SetCanAttack(false);
            }
            else
            {
                if (unit_handle == null)
                    return;
                unit_handle.SetCanAttack(false);
                unit_handle.SetCanCast(false);
                unit_handle.SetCanMove(false);
            }
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (owner == null)
                return;
            //remove the negative buffs
            if (on_enemy)
            {
                enemy_handle.SetCanMove(enemy_handle.CanMove);
                enemy_handle.SetCanCast(enemy_handle.CanCast);
                enemy_handle.SetCanAttack(enemy_handle.CanAttack);
            }
            else
            {
                unit_handle.SetCanAttack(true);
                unit_handle.SetCanCast(true);
                unit_handle.SetCanMove(true);
            }
        }


    }
}
