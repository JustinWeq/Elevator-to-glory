using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability_Script : MonoBehaviour
{
    public enum TargetType
    {
        NoTarget,
        Aura,
        PointTarget,
        AreaTarget,
        VectorTarget
    }
    public enum AbilityActivationType
    {
        AutoCast,
        Toggle,
        Channeld
    }
    GameObject parent_unit;
    public float BaseCooldown;
    public float BaseManaCost;
    public float BaseCastTime;
    public float BaseCastRange;
    public TargetType _TargetType;
    public AbilityActivationType ActivationType;
    public bool ImmunityPiercing;
    public bool NonPurgeable;
    public bool NonDispellable;
    private float remaining_cooldown;
    private float cooldown;
    private float cast_time;
    private float cast_range;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetParentUnit(GameObject parent_unit)
    {
        this.parent_unit = parent_unit;
    }

    // Update is called once per frame
    void Update()
    {
        if(remaining_cooldown > 0)
        {
            remaining_cooldown -= 1.0f*Time.deltaTime;
        }
    }

    public bool Fire()
    {
        if( remaining_cooldown <= 0.001f)
        {
            ActivateAbility();
            remaining_cooldown = cooldown;
            return true;
        }
        return false;
    }

    public void ScaleCooldown(float scale)
    {
        cooldown *= scale;
    }

    public void ScaleCastRange(float scale)
    {
        cast_range *= scale;
    }

    public void ScaleCastTime(float scale)
    {
        cast_time *= scale;
    }

    public bool OnCooldown()
    {
        return remaining_cooldown > 0;
    }

    public float GetCastTime()
    {
        return cast_time;
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public float GetRemainingCooldown()
    {
        return remaining_cooldown;
    }

    public float GetCastRange()
    {
        return cast_range;
    }

    
    protected abstract void ActivateAbility();
}
