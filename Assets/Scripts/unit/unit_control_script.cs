using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

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
    public GameObject AnimationTarget;
    public ParticleSystem BleedEffect;
    private ParticleSystem bleed_system;
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
    protected bool can_be_controlled;
    protected bool can_die;
    protected bool can_heal;
    protected bool is_dead;
    protected bool magic_immune;
    protected bool damage_immune;
    protected bool physical_damage_immune;
    protected int experience;
    protected const float MANA_SCALER = 14;
    protected const float MANA_REGEN_SCALER = 0.015f;
    protected const float HP_SCALER = 14;
    protected const float HP_REGEN_SCALER = 0.02f;
    protected const float ARMOR_SCALER = 0.2f;
    protected const float SPELL_AMP_SCALER = 0.05f;
    protected const float MovespeedScaler = 0.5f;
    protected const float BASE_MAGIC_RESIST = 25;
    public const int XP_NEEDED_FOR_LEVEL = 1000;
    protected int level;
    protected int ability_points;
    protected enemy_controller attack_target = null;
    protected unit_move_script move_Script = null;
    private List<OnHit> on_hit_list = new List<OnHit>();
    private List<OnDamaged> on_damaged_list = new List<OnDamaged>();
    private List<OnAbilityHit> on_ability_hit_list = new List<OnAbilityHit>();
    private List<Buff> registered_buffs = new List<Buff>();
    private float windup;
    private int owner_id;


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

                abilitys[i] = Instantiate(Abilities[i].GetComponent<Ability>());
                //set the abilitys parent
                abilitys[i].SetParentUnit(gameObject);
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

        //initialize starting statuses
        can_attack = true;
        can_be_controlled = true;
        can_cast_abilities = true;
        can_move = true;
        can_use_item_abilities = true;
        can_use_passives = true;
        can_die = true;
        can_heal = true;
        damage_immune = false;
        magic_immune = false;
        physical_damage_immune = false;

        //add self to player units
        level_manager.GetLevelManager().AddPlayerUnit(this);

        //set up the bleed effect system
        bleed_system = Instantiate(BleedEffect, transform.position + Vector3.up * 3, Quaternion.identity, transform).GetComponent<ParticleSystem>();
        bleed_system.Stop();

    }


    public void AddSkillPoints(int amount)
    {
        ability_points += amount;
    }

    public void AddExperience(int amount)
    {
        //level up for each amount of xp over the amount needed to level
        experience += amount;
        for (int i = 0; i < experience/XP_NEEDED_FOR_LEVEL; i++)
        {
            LevelUp();
        }
        experience %= XP_NEEDED_FOR_LEVEL;
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

    public bool ActivateAbility(int index,GameObject target)
    {
        return abilitys[index].ActivateAbility(target);
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
        return hp;
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

    public void SetCanDie(bool can_die)
    {
        this.can_die = can_die;
    }

    public bool GetCanDie(bool can_die)
    {
        return can_die;
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

    public float GetBaseArmor()
    {
        return armor;
    }

    public float GetArmor()
    {
        return armor + added_armor;
    }

    public float GetAddedArmor()
    {
        return added_armor;
    }

    public float GetMovespeed()
    {
        return movespeed;
    }

    public void add_movespeed(float amount)
    {
        added_movespeed += amount;
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

    public void AddBaseAttackTime(float amount)
    {
        BaseAttackTime += amount;
    }

    public float GetBaseAttackTime()
    {
        return BaseAttackTime;
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

    public int GetSkillPoints()
    {
        return ability_points;
    }






    // Update is called once per frame
    void Update()
    {
        if (is_dead)
            return;
        move_Script.CorrectRotation();

        //check to see if we are dead
        if (hp < 1)
        {
            if(can_die)
            {
                hp = 0;
                is_dead = true;
            }
            else
            {
                hp = 1.001f;
            }
        }

        if (can_heal)
            hp += (added_hp_regen + hp_regen)*Time.deltaTime;
        if (hp > max_hp)
            hp = max_hp;

        //regen mana
        mana += (mana_regen+added_mana_regen)*Time.deltaTime;
        if(mana > max_mana)
        {
            mana = max_mana;
        }


        //check to see if we can attack the enemy
        if(attack_target != null && Vector3.Distance(attack_target.transform.position,transform.position) < GetAttackRange()/100.0f)
        {
            windup -= Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(new Vector3(attack_target.GetPosition().x, attack_target.GetPosition().z));
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            if (!can_attack)
                ResetWindup();
        }
        else
        {
            //reset windup
            ResetWindup();
        }

        if(windup <= 0 && can_attack)
        {
            //attack the enemy target
            AttackEnemy();
        }
    }

    public void Damage(float amount,enemy_controller attacker)
    {
        if (is_dead || physical_damage_immune || damage_immune)
            return;
        //go through all of the effects
        float fixed_damage = amount;
        fixed_damage = fixed_damage * 1 - ((0.06f * GetArmor()) / (1 + 0.06f * Mathf.Abs(GetArmor())));
        foreach (OnHit effect in on_hit_list)
        {
            effect.OnHit(ref fixed_damage, attacker.gameObject);
        }
        //make the particle system activate
        bleed_system.Play();
        hp -= fixed_damage;
        if (attack_target == null)
        {
            SetAttackOrder(attacker.gameObject);
        }
    }

    public void Heal(float amount)
    {
        hp += amount;
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
        FaceTowardsTarget(attack_target.GetPosition());
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
        attack_target.GetComponent<enemy_controller>().DamageEnemy(total_damage,this);

    }

    public void FaceTowardsTarget(Vector3 target)
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(target.x, target.z));
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
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

    public void RegisterBuff(Buff buff)
    {
        registered_buffs.Add(buff);
    }

    public void DeregisterBuff(Buff buff)
    {
        registered_buffs.Remove(buff);
    }

    public void SetAttackOrder(GameObject enemy)
    {
        attack_target = enemy.GetComponent<enemy_controller>();
        //move to the enemy minus the attack range and a percentage
        move_Script.MoveTo(enemy.transform.position,GetAttackRange()/100.0f-(GetAttackRange()/100.0f)*0.10f);
        //rotate towards the target
        transform.rotation = Quaternion.LookRotation(new Vector3(attack_target.GetPosition().x,transform.position.y, attack_target.GetPosition().z));
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    public void SetPlayerId(int player_id)
    {
        owner_id = player_id;
    }

    public int GetOwnerId()
    {
        return owner_id;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetCanMove(bool can_move)
    {
        this.can_move = can_move;
    }

    public bool GetCanMove()
    {
        return can_move;
    }

    public void SetCanAttack(bool can_attack)
    {
        this.can_attack = can_attack;
    }


    public bool GetCanAttack()
    {
        return can_attack;
    }

    public void SetCanCast(bool can_cast)
    {
        this.can_cast_abilities = can_cast;
    }

    public bool GetCanCast()
    {
        return this.can_cast_abilities;
    }

    public bool GetCanOrder()
    {
        return this.can_be_controlled;
    }

    public void SetCanOrder(bool can_order)
    {
        this.can_be_controlled = can_order;
    }

    public void SetPhysicalDamageImmune(bool immune)
    {
        physical_damage_immune = immune;
    }

    public void SetDamageImmune(bool immune)
    {
        damage_immune = immune;
    }

    public void SetMagicDamageImmune(bool immune)
    {
        magic_immune = immune;
    }

    public void RemoveHp(float amount, bool physical = false)
    {
        if (physical && physical_damage_immune)
            return;

        if (magic_immune)
            return;

        hp -= amount;
    }

    public void PureDamage(float amount)
    {
        if (damage_immune)
            return;
        hp -= amount;
    }

    public bool IsDead()
    {
        return is_dead;
    }


}
