using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatDamage_script : Ability
{
    public float[] DamageAbsorbed;

    public float[] DispersionTime;
    public override bool ActivateAbility(GameObject target = null)
    {
        throw new System.NotImplementedException();
    }

    public override string GetDescription()
    {
        return "!?!?!?!?!?!?";
    }


    protected override void LevelUp()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
     void Start()
    {
        base.Start();
    }

    // Update is called once per frame
     void Update()
    {
        base.Start();
    }
}
