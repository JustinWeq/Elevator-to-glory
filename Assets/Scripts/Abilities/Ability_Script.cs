﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    NoTarget,
    Aura,
    PointTarget,
    AreaTarget,
    VectorTarget,
    Self
}
public enum AbilityActivationType
{
    Cast,
    AutoCast,
    Toggle,
    Channeled,
    Passive
}

public abstract class Ability : MonoBehaviour
{
    public string Name;
    public float[] BaseCooldown;
    public float[] BaseManaCost;
    public float BaseCastTime;
    public float[] BaseCastRange;
    public int can_be_leveled = 2;
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
    protected float mana_cost;
    protected string name;

    protected int level;
    protected GameObject parent_unit;
    private bool on_enemy;
    public int MaxLevel;



    public abstract bool ActivateAbility(GameObject target = null);

    protected abstract void LevelUp();

    public abstract string GetDescription();

     public bool Level()
     {
        if (on_enemy)
            return false;
        if (level < MaxLevel && parent_unit.GetComponent<unit_control_script>().GetLevel() > level*can_be_leveled)
        {
            level++;
            //level stats
            cooldown = BaseCooldown[level];
            mana_cost = BaseManaCost[level];
            cast_range = BaseCastRange[level];
            LevelUp();
            return true;
        }
        return false;
     }



    // Start is called before the first frame update
    protected void Start()
    {
        level = 0;

        on_enemy = false;

        name = Name;
    }


    public void SetParentUnit(GameObject parent_unit)
    {
        this.parent_unit = parent_unit;
        //decide whther we are on an enemy or a player
        if (parent_unit.GetComponent<unit_control_script>() == null)
        {
            on_enemy = true;
        }
        else
        {
            on_enemy = false;
        }
    }

    public Texture GetIcon()
    {
        return Thumbnail;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (remaining_cooldown > 0)
        {
            remaining_cooldown -= 1.0f * Time.deltaTime;
        }
    }

    public void ScaleManaCost(float scale)
    {
        mana_cost = BaseManaCost[level];
        mana_cost *= scale;
    }


    public void ScaleCooldown(float scale)
    {
        cooldown = BaseManaCost[level];
        cooldown *= scale;
    }

    public void ScaleCastRange(float scale)
    {
        cast_range = BaseCastRange[level];
        cast_range *= scale;
    }

    public void ScaleCastTime(float scale)
    {
        cast_time = BaseCastTime;
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


    public float GetCost()
    {
        return mana_cost;
    }

    public string GetName()
    {
        return name;
    }


}
