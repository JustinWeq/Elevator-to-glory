using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Cast,
    AutoCast,
    Toggle,
    Channeld
}

public abstract class Ability : MonoBehaviour
{

    public float BaseCooldown;
    public float BaseManaCost;
    public float BaseCastTime;
    public float BaseCastRange;
    public TargetType _TargetType;
    public AbilityActivationType ActivationType;
    public bool ImmunityPiercing;
    public bool NonPurgeable;
    public bool NonDispellable;
    public Texture Thumbnail;
    protected float remaining_cooldown;
    protected float cooldown;
    protected float cast_time;
    protected float cast_range;
    protected int level;
    protected GameObject parent_unit;
    public int MaxLevel;


    public abstract void ActivateAbility(GameObject target = null);

    protected abstract void LevelUp();

     public bool Level()
     {
        if (level < MaxLevel)
        {
            level++;
            LevelUp();
            return true;
        }
        return false;
     }

    // Start is called before the first frame update
    protected void Start()
    {
        level = 0;
    }


    public void SetParentUnit(GameObject parent_unit)
    {
        this.parent_unit = parent_unit;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (remaining_cooldown > 0)
        {
            remaining_cooldown -= 1.0f * Time.deltaTime;
        }
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


}
