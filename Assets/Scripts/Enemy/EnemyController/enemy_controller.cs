using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controller : MonoBehaviour
{
    public float MaxHp;
    public float HpRegen;
    public float MaxMana;
    public float ManaRegen;
    public float BaseAttackTime;
    public float Damage;
    private float hp;
    private float mana;
    public GameObject Abilties;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHp(int amount)
    {
        hp += amount;
        if (hp > MaxHp)
            hp = MaxHp;
    }

    public void AddMana(int amount)
    {
        mana += amount;
        if (mana > MaxMana)
            mana = MaxMana;
    }
}
