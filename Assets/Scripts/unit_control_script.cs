using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_control_script : MonoBehaviour
{
    public enum PrimaryAttributeType
    {
        Strength,
        Agility,
        Intelligence
    }
    public enum AttackType
    {
        Melee,
        Ranged
    }
    public float BaseStrength;
    public float BaseAgility;
    public float BaseIntelligence;
    public float BaseStrengthGain;
    public float BaseAgilityGain;
    public float BaseIntelligenceGain;
    public float BaseArmor;
    public float BaseManaRegen;
    public float BaseMana;
    public float BaseHpRegen;
    public float BaseHp;
    public float BaseAttackTime;
    public float BaseDamage;
    public float BaseMoveSpeed;
    public float BaseAttackRange;
    public PrimaryAttributeType PrimaryAttribute;
    public AttackType AttackStyle;
    public bool CustomUi = false;
    protected Ability[] abilitys = new Ability[6];
    protected float hp;
    protected float max_mana;
    protected float max_hp;
    protected float mana;
    protected float movespeed;
    protected float armor;
    protected float attack_speed;
    protected float intelligence;
    protected float strength;
    protected float agility;
    protected float hp_regen;
    protected float mana_regen;
    protected float damage;
    protected const float MANA_SCALER = 14;
    protected const float MANA_REGEN_SCALER = 0.015f;
    protected const float HP_SCALER = 14;
    protected const float HP_REGEN_SCALER = 0.02f;
    protected int level;
    protected int ability_points;



    // Start is called before the first frame update
    void Start()
    {
        //set level and give one ability point
        level = 1;
        ability_points = 1;
        //set stats

        //calculate starting hp
        hp = BaseHp + HP_SCALER * BaseStrength;
        mana = BaseMana + MANA_SCALER * BaseIntelligence;

        //set max mana and hp
        max_hp = hp;
        max_mana = mana;
    }

    public bool LevelAbility(int index)
    {
        if(ability_points > 0)
        {
            if(abilitys[index] != null && abilitys[index].Level())
            {
                ability_points--;
                return true;
            }
        }
        return false;
    }

    public float GetMaxMana()
    {
        return max_mana;
    }

    public float GetCurrentMana()
    {
        return mana;
    }

    public float GetMaxHp()
    {
        return max_hp;
    }

    public float GetHp()
    {
        return max_hp;
    }

    public void AddMana(float amount)
    {
        mana += amount;

        if (mana < 0)
            mana = 0;
        else if (mana > max_mana)
            mana = max_mana;
    }

    public void AddHp(float amount)
    {
        hp += amount;
        if (hp < 0)
            hp = 0;
        else if(hp > max_hp)
            hp = max_hp;
    }



    // Update is called once per frame
    void Update()
    {
        //check to see if any ability keys are being pressed
        if(Input.GetKeyDown("q"))
        {
            if (abilitys[0] != null)
                abilitys[0].ActivateAbility();
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("w"))
        {
            if (abilitys[1] != null)
                abilitys[1].ActivateAbility();
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("e"))
        {
            if (abilitys[2] != null)
                abilitys[2].ActivateAbility();
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("r"))
        {
            if (abilitys[3] != null)
                abilitys[3].ActivateAbility();
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("f"))
        {
            if (abilitys[4] != null)
                abilitys[4].ActivateAbility();
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("d"))
        {
            if (abilitys[5] != null)
                abilitys[5].ActivateAbility();
        }



    }

    
}
