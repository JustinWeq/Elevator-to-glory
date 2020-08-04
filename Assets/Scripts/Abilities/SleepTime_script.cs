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
        throw new System.NotImplementedException();
    }

    protected override string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    protected override string GetName()
    {
        throw new System.NotImplementedException();
    }

    protected override void LevelUp()
    {
        heal_amount = HealAmount[level];
        sleep_duration = SleepDuration[level];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
