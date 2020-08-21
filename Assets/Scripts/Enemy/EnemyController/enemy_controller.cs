using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemy_controller : MonoBehaviour
{
    public float MaxHp;
    public float HpRegen;
    public float MaxMana;
    public float ManaRegen;
    public float BaseAttackTime;
    public float Damage;
    public float Armor;
    public float MoveSpeed;
    public float HealthBarHeight = 2.0f;
    public float AttackRange;
    public float MagicResist;
    public bool CanAttack = true;
    public bool CanMove = true;
    public bool CanCast = true;
    public bool _CanBeAttacked = true;
    public bool MagicImmune = false;
    public bool PhysicalImmune = false;
    public bool CanDie = true;
    public float DetectionRange;
    public GameObject BleedEffect;
    private float hp;
    private float mana;
    public bool Captain;
    public GameObject[] Abilties;
    public GameObject HpBar;
    protected GameObject hp_bar;
    protected Camera cam;
    protected player_controller_script player_controller;
    protected hp_bar_controller hp_controller;
    protected bool can_be_attacked;
    protected bool can_attack;
    protected bool can_move;
    protected bool can_cast;
    protected bool is_dead;
    protected bool is_captain;
    protected bool can_be_controlled;
    protected bool magic_immune;
    protected bool physical_immune;
    protected bool can_die;
    protected unit_control_script target;
    protected unit_control_script heading_target;
    protected ParticleSystem bleed_system;
    protected NavMeshAgent nav_agent;
    protected float windup;
    protected List<Buff> buffs = new List<Buff>();

    // Start is called before the first frame update
    void Awake()
    {
        can_be_attacked = _CanBeAttacked;
        can_attack = CanAttack;
        can_move = CanMove;
        can_cast = CanCast;
        is_dead = false;
        can_be_controlled = true;
        is_captain = Captain;
    }

    private void Start()
    {
        //set the hp to full
        hp = MaxHp;
        //create the hp bar
        hp_bar = Instantiate(HpBar);
        //create the bleed effect
        bleed_system = Instantiate(BleedEffect, transform.position + Vector3.up * HealthBarHeight, Quaternion.identity, transform).GetComponent<ParticleSystem>();
        bleed_system.Stop();
        //Get the player controller
        //player_controller = GameObject.Find("PlayerController").GetComponent<player_controller_script>();
        //get the camera
        cam = GameObject.FindObjectOfType<Camera>();
        hp_controller = hp_bar.GetComponent<hp_bar_controller>();
        hp_controller.SetHpAndMaxHp(hp, MaxHp);
        //set default values
        can_attack = CanAttack;
        can_be_attacked = _CanBeAttacked;
        can_move = CanMove;
        can_cast = CanCast;
        magic_immune = MagicImmune;
        physical_immune = PhysicalImmune;
        can_die = CanDie;
        //make sure the enemy is alive
        is_dead = false;

        //set the target to null
        target = null;

        GlobalManager.GetGlobalManager().GetLevelManager().AddEnemy(this);

        //get the nav agent
        if (can_attack)
            nav_agent = GetComponent<NavMeshAgent>();
    }

    public void RemoveHp(float amount,bool physical = false)
    {
        if (physical && physical_immune)
            return;

        if (magic_immune)
            return;

        hp -= amount;
    }

    public void PureDamage(float amount)
    {
        if (can_be_attacked)
            return;
        hp -= amount;
    }



    private void OnDestroy()
    {
        //rmeove self from the level manager
        level_manager.GetLevelManager().RemoveEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        int reee;
        if (can_move == !CanMove)
            reee = 3;
        if(is_dead)
        {
            return;
        }
        if (hp < 0.0001)
        {
            hp = 0;
            gameObject.AddComponent<DieAndFall>().Duration = 2.0f;
            can_be_attacked = false;
            //destroy hp bar
            Destroy(hp_bar);
            Destroy(gameObject.GetComponent<NavMeshAgent>());
            Destroy(gameObject.GetComponent<CapsuleCollider>());
            Destroy(gameObject.GetComponent<NavMeshObstacle>());
            is_dead = true;
            return;
        }
        //set hp bar position
        //hp_bar_back.transform.position = cam.WorldToScreenPoint(transform.position);
        hp_controller.SetPosition(transform.position + Vector3.up*HealthBarHeight);
        //update the hp bars hp
        hp_controller.SetHpAndMaxHp(hp,MaxHp);

        //decide whether we can attack the player or noy
        //set attack target to something in range if we dont have one
        if(can_attack && heading_target == null && target == null)
        {
            //get the units in range and choose one randomly
            List<unit_control_script> units_in_range = GlobalManager.GetGlobalManager().GetLevelManager().GetPlayerUnitsInRange(GetPosition(), DetectionRange);
            if (units_in_range.Count > 0)
                heading_target = units_in_range[Random.Range(0, units_in_range.Count)];
            else
                heading_target = null;
        }

        //Check to see if there are any units in attack range
        if(can_attack && target == null)
        {
            List<unit_control_script> units_in_range = GlobalManager.GetGlobalManager().GetLevelManager().GetPlayerUnitsInRange(GetPosition(), AttackRange);
            if (units_in_range.Count > 0)
                target  = units_in_range[Random.Range(0, units_in_range.Count)];
        }

        if(can_attack && target != null)
        {
            SetAttackOrder(target);
        }
        else if(can_attack && heading_target != null)
        {
            SetAttackOrder(heading_target);
        }

        ////get the nav agent
        //if (can_attack && nav_agent == null)
        //    nav_agent = GetComponent<NavMeshAgent>();
        //set the movespeed and acceleration
        if (can_move)
        {
            nav_agent.speed = MoveSpeed / 10;
            nav_agent.angularSpeed = MoveSpeed;
            nav_agent.acceleration = MoveSpeed;
        }
        else
        {
            if (nav_agent != null)
            {
                nav_agent.speed = 0;
                nav_agent.acceleration = 0;
            }
        }

        //check to see if we can attack the player
        if (can_attack && target != null && Vector3.Distance(target.transform.position, transform.position) < AttackRange / 100.0f)
        {
            windup -= Time.deltaTime;
        }
        else
        {
            //reset windup
            windup = BaseAttackTime;
        }

        if (can_attack && windup <= 0)
        {
            //attack the enemy target
            target.Damage(Damage,this);
            //reset windup
            windup = BaseAttackTime;
        }
    }

    public void AddHp(int amount)
    {
        hp += amount;
        if (hp > MaxHp)
            hp = MaxHp;
    }

    public float GetHp()
    {
        return hp;
    }

    public bool GetCanDie()
    {
        return can_die;
    }

    public void SetCanDie(bool can_die)
    {
        this.can_die = can_die;
    }

    public void SetCanBeControlled(bool can_be_controlled)
    {
        this.can_be_controlled = can_be_controlled;
    }

    public bool GetCanBeControlled()
    {
        return can_be_controlled;
    }
    public void DamageEnemy(float amount,unit_control_script attacker)
    {
        if (is_dead|| !can_be_attacked)
            return;
        //make the particle system activate
        bleed_system.Play();
        hp -= amount * 1 - ((0.06f * Armor) / (1 + 0.06f * Mathf.Abs(Armor)));
        if(target == null)
        {
            target = attacker;
        }
    }

    public void Heal(float amount)
    {
        hp += amount;
    }

    public void MagicDamage(float amount,unit_control_script attacker)
    {
        if(!is_dead && !magic_immune)
        {
            float total_damage = amount;
            //Add spell amp
            total_damage += total_damage*attacker.GetSpellamp() / 100.0f;
            total_damage -= total_damage * (MagicResist / 100.0f);
            //reduce hp by the amount of magic damage being done
            hp -= total_damage;
        }
    }

    public void AddMana(int amount)
    {
        mana += amount;
        if (mana > MaxMana)
            mana = MaxMana;
    }

    public bool CanBeAttacked()
    {
        return can_be_attacked;
    }

    public void SetCanAttack(bool can_attack)
    {
        this.can_attack = can_attack;
    }

    public bool GetCanBeAttacked()
    {
        return can_be_attacked;
    }

    public void SetCanBeAttack(bool can_be_attacked)
    {
        this.can_be_attacked = can_be_attacked;
    }

    public void SetCanMove(bool can_move)
    {
        this.can_move = can_move;
    }

    public bool GetCanMove()
    {
        return can_move;
    }

    public void SetCanCast(bool can_cast)
    {
        this.can_cast = can_cast;
    }

    public bool GetCanCast()
    {
        return this.can_cast;
    }

    public void SetPhysicalImmune(bool immune)
    {
        physical_immune = immune;
    }

    public bool GetPhysicalImmune()
    {
        return physical_immune;
    }

    public void RegisterBuff(Buff buff)
    {
        buffs.Add(buff);
    }

    public void DeregisterBuff(Buff buff)
    {
        buffs.Remove(buff);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetAttackOrder(unit_control_script target)
    {
        //set the nav agent target to the attack target
        nav_agent.destination = target.GetPosition();
        nav_agent.stoppingDistance = (AttackRange/100) - AttackRange / 1000.0f;
    }

    public void MoveToo(Vector3 position)
    {
        nav_agent.destination = position;
    }

    public bool IsCaptain()
    {
        return is_captain;
    }

    public bool IsDead()
    {
        return is_dead;
    }


}
