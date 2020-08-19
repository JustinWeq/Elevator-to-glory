using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Buffs.CluelessBuffs
{
    class WhatDamageStack : Buff
    {
        float damage_absorbed;
        protected override void BuffEffect()
        {
            //add the absorbed damage
            if(on_enemy)
            {
                enemy_handle.RemoveHp((damage_absorbed/start_time)*Time.deltaTime,true);
            }
            else
            {
                unit_handle.RemoveHp((damage_absorbed / start_time) * Time.deltaTime, true);
            }
        }


        public void SetDamageAbsorbed(float amount)
        {
            damage_absorbed = amount;
        }

    }
}
