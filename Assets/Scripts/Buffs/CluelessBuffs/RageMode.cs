using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Buffs.CluelessBuffs
{
    class RageMode : Buff
    {
        private float minimum_hp;
        private float max_base_attack_time_reduction;
        private float min_base_attack_time_reduction;
        private float move_speed_buff;
        private float last_move_speed_buff = 0;
        private float last_attack_speed_buff = 0;
        protected override void BuffEffect()
        {
            if(on_enemy)
            {
                if(enemy_handle.GetHp() < minimum_hp)
                {
                    enemy_handle.Heal(minimum_hp - enemy_handle.GetHp());
                }
                enemy_handle.SetCanBeControlled(false);
                enemy_handle.SetCanDie(false);
            }
            else
            {
                if (unit_handle.GetHp() < minimum_hp)
                {
                    unit_handle.Heal(minimum_hp - unit_handle.GetHp());
                }
                unit_handle.add_movespeed(last_move_speed_buff);
                unit_handle.SetCanOrder(false);
                unit_handle.SetCanDie(false);
                //calculate the movespeed buff
                last_move_speed_buff = (unit_handle.GetMovespeed() + unit_handle.GetAddedMovespeed()) * move_speed_buff;
                //apply the movespeedbuff
                unit_handle.add_movespeed(-last_move_speed_buff);

                //calculate the attack time buff
                unit_handle.AddBaseAttackTime(last_attack_speed_buff);
                last_attack_speed_buff = (time_left / start_time) * (max_base_attack_time_reduction - min_base_attack_time_reduction) + min_base_attack_time_reduction;
                unit_handle.AddBaseAttackTime(-last_attack_speed_buff);
            }
        }
        
        public void SetParameters(float minimum_hp,float max_attack_time,float min_attack_time,float move_speed_buff)
        {
            this.minimum_hp = minimum_hp;
            this.max_base_attack_time_reduction = max_attack_time;
            this.min_base_attack_time_reduction = min_attack_time;
            this.move_speed_buff = move_speed_buff;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (on_enemy)
            {
                enemy_handle.SetCanBeControlled(true);
                enemy_handle.SetCanDie(true);
            }
            else
            {
                unit_handle.SetCanOrder(true);
                unit_handle.SetCanDie(true);
                unit_handle.add_movespeed(last_move_speed_buff);
                unit_handle.AddBaseAttackTime(last_attack_speed_buff);
                //try and cast sleep if you have it
                for (int i = 0; i < 3; i++)
                {
                    //get the owners abilitys
                    Ability ability = unit_handle.GetAbility(i);
                    if (ability is SleepTime_script)
                    {
                        ability.ActivateAbility();
                        i = 4;
                    }
                }
            }
        }
    }
}
