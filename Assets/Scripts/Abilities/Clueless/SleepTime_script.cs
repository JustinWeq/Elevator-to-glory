using Assets.Scripts.Buffs.CluelessBuffs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepTime_script : Ability 
{
    public float[] SleepDuration;
    public float[] HealAmount;
    private float sleep_duration;
    private float heal_amount;
    public override bool ActivateAbility(GameObject target = null)
    {
        //add a SleepModeBuff to the player
        SleepModeBuff buff = Instantiate(new GameObject()).AddComponent<SleepModeBuff>();
        buff.SetDuration(sleep_duration);
        buff.SetHealAmount(heal_amount);
        buff.SetOwner(parent_unit);
        remaining_cooldown = cooldown;
        return true;
    }

    public override string GetDescription()
    {
        return "Clueless falls asleep for (" + sleep_duration + ") and heals ("+ heal_amount
            +") over the duration, Clueless is immune to physical damage during this time.";
    }


    protected override void LevelUp()
    {
        heal_amount = HealAmount[GetLevel()-1];
        sleep_duration = SleepDuration[GetLevel()-1];
    }

}
