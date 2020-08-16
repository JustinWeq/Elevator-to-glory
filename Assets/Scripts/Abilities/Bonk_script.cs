using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonk_script :   Ability
{

    public float[] StunDuration = new float[6];
    public float[] DamageAmount = new float[6];

    private float damage;
    private float stun_duration;


    public override bool ActivateAbility(GameObject target)
    {
        if (remaining_cooldown <= 0.001f)
        {
            //activate the ability here


            remaining_cooldown = cooldown;

        }
        return true;
    }

    public override string GetDescription()
    {
        return "Bonk!!!!!";
    }

    protected override void LevelUp()
    {
        //level the ability
        damage = DamageAmount[level];
        stun_duration = StunDuration[level];
    }

}
