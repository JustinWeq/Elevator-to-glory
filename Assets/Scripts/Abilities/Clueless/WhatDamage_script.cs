using Assets.Scripts;
using Assets.Scripts.Buffs.CluelessBuffs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatDamage_script : Ability,OnHit
{
    public float[] DamageAbsorbed;
    private float damage_absorbed;

    public float[] DispersionTime;
    private float dispersion_time;
    public override bool ActivateAbility(GameObject target = null)
    {
        
        return true;
    }

    public override string GetDescription()
    {
        return "Physical damage is instead absorbed and taken as a damage over time effect. (% " + damage_absorbed +
            ") of damage is instead taken as a damage over time of (" + dispersion_time + ") seconds.";
    }


    protected override void LevelUp()
    {
        damage_absorbed = DamageAbsorbed[GetLevel()-1];
        dispersion_time = DispersionTime[GetLevel()-1];
    }

    public void OnHit(ref float damage, GameObject attacker)
    {
        if (!toggled_on)
            return;
        if (GetLevel() < 1)
            return;
        //absorb the damage
        GameObject emptyObj = new GameObject();
        WhatDamageStack stack = Instantiate(emptyObj).AddComponent<WhatDamageStack>();
        stack.SetDuration(dispersion_time);
        float damage_a = (damage_absorbed / 100.0f) * damage;
        stack.SetDamageAbsorbed(damage_a);
        stack.SetOwner(parent_unit);
        damage -= damage_a;
    }

    new public void Toggle()
    {
        base.Toggle();
        if(toggled_on)
        {
            if(on_enemy)
            {
                //do nothinng for now
            }
            else
            {
                //add on hit effect to player
                unit_control_handle.RegisterOnHit(this);
            }
        }
        else
        {
            //remove on hit effect
            if(on_enemy)
            {
                //remove for now
            }
            else
            {
                //deregister the effect
                unit_control_handle.DeregisterOnHit(this);
            }
        }
    }


    new void Start()
    {
        base.Start();
        
    }

    public override void SetParentUnit(GameObject owner)
    {
        base.SetParentUnit(owner);
        if(!on_enemy)
        {
            unit_control_handle.RegisterOnHit(this);
        }
    }
}
