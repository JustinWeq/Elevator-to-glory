using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    protected float added_hp;
    protected float max_mana;
    protected float added_max_mana;
    protected float max_hp;
    protected float added_max_hp;
    protected float mana;
    protected float added_mana;
    protected float movespeed;
    protected float added_movespeed;
    protected float armor;
    protected float added_armor;
    protected float attack_speed;
    protected float added_attack_speed;
    protected float intelligence;
    protected float strength;
    protected float agility;
    protected float hp_regen;
    protected float added_hp_regen;
    protected float mana_regen;
    protected float added_mana_regen;
    protected float spell_amp;
    protected float added_spell_amp;
    protected float damage;
    protected float added_damage;
    protected float magic_resistance;
    protected float added_magic_resistance;
    protected float castspeed_reduction;
    protected float cooldown_reduction;
    protected float cast_range;
    protected float spell_lifesteal;
    protected float lifesteal;
    protected float attack_range;
    protected float pure_damage;
    protected float splash;
    protected float cleave;
    protected float critical_damage;
    protected float critical_chance;
    protected float status_resist;
    protected bool can_move;
    protected bool can_attack;
    protected bool can_cast_abilities;
    protected bool can_use_passives;
    protected bool can_use_item_abilities;
    protected int experience;
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
    protected GameObject attack_target = null;
    protected unit_move_script move_Script = null;
    private List<OnHit> on_hit_list;
    private List<OnDamaged> on_damaged_list;
    private List<OnAbilityHit> on_ability_hit_list;
    private float windup;



    // Start is called before the first frame update
    void Start()
    {
        //get the unit move script
        move_Script = GetComponent<unit_move_script>();
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

        //set base movespeed
        movespeed = BaseMoveSpeed + MovespeedScaler*agility;

        //read in the abilitys
        for(int i = 0;i < 6;i++)
        {
            if(Abilities[i] != null)
            {
                abilitys[i] = Abilities[i].GetComponent<Ability>();
            }
        }

        LevelUp();
        //set experience
        experience = 0;
        //set added stats
        added_attack_speed = 0;
        added_armor = 0;
        added_damage = 0;
        added_hp = 0;
        added_hp_regen = 0;
        added_magic_resistance = 0;
        added_mana = 0;
        added_mana_regen = 0;
        added_max_hp = 0;
        added_max_mana = 0;
        added_movespeed = 0;
        added_spell_amp = 0;

        //set extra stats
        castspeed_reduction = 0.0f;
        cooldown_reduction = 0.0f;
        cast_range = 0;
        spell_lifesteal = 0;
        lifesteal = 0;
        attack_range = 0;
        pure_damage = 0;
        splash = 0;
        cleave = 0;
        added_magic_resistance = 0;
        status_resist = 0;
        critical_damage = 1;
        critical_chance = 0;

        //init lists
        on_ability_hit_list = new List<OnAbilityHit>();
        on_damaged_list = new List<OnDamaged>();
        on_hit_list = new List<OnHit>();
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

    public float GetBaseDamage()
    {
        return damage;
    }

    public float GetDamage()
    {
        return BaseDamage + added_damage;
    }
    public float GetAddedDamage()
    {
        return added_damage;
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

    public float GetAddedHpRegen()
    {
        return added_hp_regen;
    }

    public float GetManaRegen()
    {
        return mana_regen;
    }

    public float GetAddedManaRegen()
    {
        return added_mana_regen;
    }

    public int GetAttackSpeed()
    {
        return (int)attack_speed;
    }

    public int GetAddedAttackSpeed()
    {
        return (int)added_attack_speed;
    }

    public float GetArmor()
    {
        return armor;
    }

    public float GetAddedArmor()
    {
        return added_armor;
    }

    public float GetMovespeed()
    {
        return movespeed;
    }

    public float GetAddedMovespeed()
    {
        return added_movespeed;
    }

    public float GetSpellamp()
    {
        return spell_amp;
    }

    public float GetAddedSpellAmp()
    {
        return added_spell_amp;
    }

    public float GetCastSpeedReduction()
    {
        return castspeed_reduction;
    }

    public float GetCooldownReduction()
    {
        return cooldown_reduction;
    }

    public float GetCastRange()
    {
        return cast_range;
    }

    public float GetBaseAttackRange()
    {
        return BaseAttackRange;
    }

    public float GetAddedAttackRange()
    {
        return attack_range;
    }

    public float GetAttackRange()
    {
        return BaseAttackRange + attack_range;
    }

    public float GetSpellLifesteal()
    {
        return spell_lifesteal;
    }

    public float GetLifesteal()
    {
        return lifesteal;
    }

    public float GetPureDamage()
    {
        return pure_damage;
    }

    public float GetSplash()
    {
        return splash;
    }

    public float GetCleave()
    {
        return cleave;
    }

    public float GetMagicResist()
    {
        return magic_resistance;
    }

    public float GetAddedMagicResistance()
    {
        return added_magic_resistance;
    }

    public float GetStatusResist()
    {
        return status_resist;
    }

    public float GetCriticalDamage()
    {
        return critical_damage;
    }

    public float GetCriticalChance()
    {
        return critical_chance;
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


        //check to see if we can attack the enemy
        if(attack_target != null && Vector3.Distance(attack_target.transform.position,transform.position) < GetAttackRange()/100.0f)
        {
            windup -= Time.deltaTime; 
        }
        else
        {
            //reset windup
            ResetWindup();
        }

        if(windup <= 0)
        {
            //attack the enemy target
            AttackEnemy();
        }
    }

    private void ResetWindup()
    {
        windup = GetAttackTime();
    }

    public float GetAttackTime()
    {
        return BaseAttackTime / (1.0f + attack_speed/100.0f);
    }

    private void AttackEnemy()
    {
        //set wind up back to start
        ResetWindup();
        //calculate damage
        float total_damage = GetDamage();
        //calculate crit
        int crit_multiply = 0;
        crit_multiply +=(int) (critical_chance / 100);
        crit_multiply += Random.Range(1, 100) < (int)(critical_chance) % 100?1:0;
        //add crit damage
        total_damage += total_damage * crit_multiply * critical_damage;
        total_damage += pure_damage;
        //damage the enemy target
        attack_target.GetComponent<enemy_controller>().DamageEnemy(total_damage);

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

    public void SetAttackOrder(GameObject enemy)
    {
        attack_target = enemy;
        //move to the enemy minus the attack range and a percentage
        move_Script.MoveTo(enemy.transform.position,GetAttackRange()/100.0f-(GetAttackRange()/100.0f)*0.10f);
        //rotate towards the target
        transform.LookAt(new Vector3(enemy.transform.position.x,enemy.transform.position.y,enemy.transform.position.z), -Vector3.up);
    }

}
