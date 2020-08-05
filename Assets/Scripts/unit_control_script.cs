using Assets.Scripts;
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
    public GameObject[] Abilities = new GameObject[6];
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
    protected float spell_amp;
    protected float damage;
    protected float magic_resistance;
    protected const float MANA_SCALER = 14;
    protected const float MANA_REGEN_SCALER = 0.015f;
    protected const float HP_SCALER = 14;
    protected const float HP_REGEN_SCALER = 0.02f;
    protected const float ARMOR_SCALER = 0.2f;
    protected const float SPELL_AMP_SCALER = 0.05f;
    protected const float MovespeedScaler = 0.5f;
    protected const float BASE_MAGIC_RESIST = 25;
    protected int level;
    protected int ability_points;
    private List<OnHit> on_hit_list;
    private List<OnDamaged> on_damaged_list;




    // Start is called before the first frame update
    void Start()
    {
        //set level and give one ability point
        level = 0;
        ability_points = 0;
        //set stats

        //calculate starting hp
        hp = BaseHp;// + HP_SCALER * BaseStrength;
        mana = BaseMana;// + MANA_SCALER * BaseIntelligence;

        //set max mana and hp
        max_hp = hp;
        max_mana = mana;

        //set starting regen
        hp_regen = BaseHpRegen;// + strength * HP_REGEN_SCALER;
        mana_regen = BaseManaRegen;// + intelligence*MANA_REGEN_SCALER;

        //set starting stats
        intelligence = BaseIntelligence;
        agility = BaseAgility;
        strength = BaseStrength;

        //set starting armor
        armor = BaseArmor;// + agility * ARMOR_SCALER;
        //set starting attack speed
        attack_speed = agility;
        //set starting damage
        damage = BaseDamage;
        switch(PrimaryAttribute)
        {
            case PrimaryAttributeType.Agility:
            {
                damage += agility;
                break;
            }
            case PrimaryAttributeType.Strength:
            {
                    damage += strength;
               break;
            }
            case PrimaryAttributeType.Intelligence:
            {
                    damage += intelligence;
               break;
            }
        }

        //set spell amp
        spell_amp = intelligence * SPELL_AMP_SCALER;

        //set base spell resist
        magic_resistance = BASE_MAGIC_RESIST;

        //read in the abilitys
        for(int i = 0;i < 6;i++)
        {
            if(Abilities[i] != null)
            {
                abilitys[i] = Abilities[i].GetComponent<Ability>();
            }
        }
        LevelUp();
        LevelUp();
        LevelUp();
        LevelUp();
        LevelUp();
        LevelUp();
    }

    public void LevelUp()
    {
        //level up
        level++;
        ability_points++;

        //update hp
        hp += HP_SCALER *BaseStrengthGain;
        max_hp += HP_SCALER * BaseStrengthGain;
        hp_regen += HP_REGEN_SCALER * BaseStrengthGain;


        //update mana
        mana += MANA_SCALER * BaseIntelligenceGain;
        max_mana += MANA_SCALER * BaseIntelligenceGain;
        mana_regen += MANA_REGEN_SCALER * BaseIntelligenceGain;

        //update stats
        intelligence += BaseIntelligenceGain;
        strength += BaseStrengthGain;
        agility += BaseAgilityGain;

        //update damage
        switch (PrimaryAttribute)
        {
            case PrimaryAttributeType.Agility:
                {
                    damage += BaseAgilityGain;
                    break;
                }
            case PrimaryAttributeType.Strength:
                {
                    damage += BaseStrengthGain;
                    break;
                }
            case PrimaryAttributeType.Intelligence:
                {
                    damage += BaseIntelligenceGain;
                    break;
                }
        }

        //update attack speed
        attack_speed += BaseAgilityGain;

        //update armor
        armor += BaseAgilityGain * ARMOR_SCALER;

        //update movespeed
        movespeed += BaseAgilityGain * MovespeedScaler;

        //update spell amp
        spell_amp += BaseIntelligenceGain * SPELL_AMP_SCALER;
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

    public Ability GetAbility(int index)
    {
        return abilitys[index];
    }

    public float GetMaxMana()
    {
        return max_mana;
    }

    public float GetMana()
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

    public int GetLevel()
    {
        return level;
    }

    public int GetStrength()
    {
        return (int)strength;
    }

    public int GetAgility()
    {
        return (int)strength;
    }

    public int GetIntelligence()
    {
        return (int)intelligence;
    }

    public float GetHpRegen()
    {
        return hp_regen;
    }

    public float GetManaRegen()
    {
        return mana_regen;
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

    public void RegisterOnHit(OnHit effect)
    {
        on_hit_list.Add(effect);
    }

    public void DeregisterOnHit(OnHit effect)
    {
        on_hit_list.Remove(effect);
    }

    public void RegisterOnDamaged(OnDamaged effect)
    {
        on_damaged_list.Add(effect);
    }

    public void DeregisterOnDamaged(OnDamaged effect)
    {
        on_damaged_list.Remove(effect);
    }


}
