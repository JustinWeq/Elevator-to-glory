using System;
using System.Collections;
using System.Collections.Generic;
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
    public bool CanAttack = true;
    public bool CanMove = true;
    public bool CanCast = true;
    public bool _CanBeAttacked = true;
    public GameObject BleedEffect;
    private float hp;
    private float mana;
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
    protected unit_control_script target;
    protected ParticleSystem bleed_system;
   // protected RawImage hp_bar_front;
   // protected RawImage hp_bar_back;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
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
        //make sure the enemy is alive
        is_dead = false;
    }
    // Update is called once per frame
    void Update()
    {
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
    }

    public void AddHp(int amount)
    {
        hp += amount;
        if (hp > MaxHp)
            hp = MaxHp;
    }

    public void DamageEnemy(float amount)
    {
        if (hp <= 0)
            return;
        //make the particle system activate
        bleed_system.Play();
        hp -= amount * 1 - ((0.06f * Armor) / (1 + 0.06f * Math.Abs(Armor)));
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

    public void SetCanBeAttack(bool can_be_attacked)
    {
        this.can_be_attacked = can_be_attacked;
    }

    public void SetCanMove(bool can_move)
    {
        this.can_move = can_move;
    }

    public void SetCanCast(bool can_cast)
    {
        this.can_cast = can_cast;
    }

    public void RegisterBuff(Buff buff)
    {

    }

    public void DeregisterBuff(Buff buff)
    {

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
