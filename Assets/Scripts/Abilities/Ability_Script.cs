using System.Collections;
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

public enum AbilityType
{
    Nuke,
    Stun,
    Buff,
    Heal,
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
    public int CanBeLeveledAt = 1;
    public TargetType _TargetType;
    public AbilityActivationType ActivationType;
    public AbilityType _AbilityType;
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
    protected bool toggled_on;

    private int level;
    protected GameObject parent_unit;
    protected unit_control_script unit_control_handle;
    protected enemy_controller enemy_control_handle;
    protected bool on_enemy;
    public int MaxLevel;

    public Ability()
    {
        level = 0;
        toggled_on = true;
    }

    public abstract bool ActivateAbility(GameObject target = null);

    protected abstract void LevelUp();

    public abstract string GetDescription();

     public bool Level()
     {
        if (on_enemy)
            return false;
        if (CanLevel())
        {
            level++;
            //level stats
            cooldown = BaseCooldown[level-1];
            mana_cost = BaseManaCost[level-1];
            cast_range = BaseCastRange[level-1];
            LevelUp();
            return true;
        }
        return false;
     }

    public int GetLevel()
    {
        return level;
    }

    public bool CanLevel()
    {
        return level < MaxLevel && unit_control_handle.GetLevel() >= CanBeLeveledAt && unit_control_handle.GetLevel() >= (level+1)* can_be_leveled;
    }

    public int GetMaxLevel()
    {
        return MaxLevel;
    }


    // Start is called before the first frame update
    protected void Start()
    {
        level = 0;

        on_enemy = false;

        name = Name;
    }


    public virtual void SetParentUnit(GameObject parent_unit)
    {
        this.parent_unit = parent_unit;
        //decide whther we are on an enemy or a player
        if (parent_unit.GetComponent<unit_control_script>() == null)
        {
            on_enemy = true;
            enemy_control_handle = parent_unit.GetComponent<enemy_controller>();
        }
        else
        {
            on_enemy = false;
            unit_control_handle = parent_unit.GetComponent<unit_control_script>();
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
            remaining_cooldown -= Time.deltaTime;
        }
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

    public void Toggle()
    {
        toggled_on = !toggled_on;
    }


}
