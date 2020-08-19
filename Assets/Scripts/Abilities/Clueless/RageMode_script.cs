using Assets.Scripts.Buffs.CluelessBuffs;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class RageMode_script : Ability
{
    public float[] Duration;
    private float duration;

    public float[] MinimumHp;
    private float minimum_hp;

    public int MinAttackSpeedReduction;
    public int MaxAttackSpeedReduction;
    public float MoveSpeedIncrease;
    public override bool ActivateAbility(GameObject target = null)
    {
        RageMode buff = Instantiate(new GameObject()).AddComponent<RageMode>();
        buff.SetDuration(duration);
        buff.SetParameters(minimum_hp, MinAttackSpeedReduction/100.0f, MaxAttackSpeedReduction/100.0f, MoveSpeedIncrease/100.0f);
        buff.SetOwner(parent_unit);
        remaining_cooldown = cooldown;
        return true;
    }

    public override string GetDescription()
    {
        return "For (" + duration + ") seconds clueless becomes uncontrollable(He attacks the nearest attackable enemy) his" +
            " base attack time lowers from " + MinAttackSpeedReduction + "% at the begining to " + MaxAttackSpeedReduction + "% at the end." +
            "Clueless will have his movespeed increased by " + MoveSpeedIncrease + "% and cannot have lower hp then (" + minimum_hp +
            ") durinng the diration of his ult, at the end clueless will instantly activate sleep.";
    }

    protected override void LevelUp()
    {
        duration = Duration[GetLevel()-1];
        minimum_hp = MinimumHp[GetLevel()-1];
    }

}
