using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Buffs.CluelessBuffs
{
    class SleepModeBuff : Buff
    {
        protected float heal_amount;
        protected override void BuffEffect()
        {
            //Heal the unit
            if (on_enemy)
            {
                enemy_handle.Heal((heal_amount / start_time) * Time.deltaTime);
                //disable the units commands
                enemy_handle.SetCanAttack(false);
                enemy_handle.SetCanMove(false);
                enemy_handle.SetCanCast(false);
            }
            else
            {
                unit_handle.Heal((heal_amount / start_time) * Time.deltaTime);
                unit_handle.SetCanCast(false);
                unit_handle.SetCanAttack(false);
                unit_handle.SetCanMove(false);
                unit_handle.SetPhysicalDamageImmune(true);
            }
        }


        public void SetHealAmount(float amount)
        {
            heal_amount = amount;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if(on_enemy)
            {
                enemy_handle.SetCanAttack(true);
                enemy_handle.SetCanMove(true);
                enemy_handle.SetCanCast(true);
            }
            else
            {
                unit_handle.SetCanCast(true);
                unit_handle.SetCanAttack(true);
                unit_handle.SetCanMove(true);
                unit_handle.SetPhysicalDamageImmune(false);
            }
        }
    }
}
