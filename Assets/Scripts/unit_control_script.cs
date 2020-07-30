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
    private Ability_Script[] abilitys = new Ability_Script[6];
    private float hp;
    private float mana;
    private float movespeed;
    private float armor;
    private float attack_speed;
    private float intelligence;
    private float strength;
    private float agility;
    private float hp_regen;
    private float mana_regen;
    private float damage;
    private float hp_scaler;
    private const float MANA_SCALER = 14;
    private const float MANA_REGEN_SCALER = 0.015f;
    private const float HP_SCALER = 14;
    private const float HP_REGEN_SCALER = 0.02f;




    // Start is called before the first frame update
    void Start()
    {

        //set stats
        hp = BaseHp + hp_scaler * BaseStrength;
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if any ability keys are being pressed
        if(Input.GetKeyDown("Q"))
        {
            if (abilitys[0] != null)
                abilitys[0].Fire();
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("W"))
        {
            if (abilitys[1] != null)
                abilitys[1].Fire();
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("E"))
        {
            if (abilitys[2] != null)
                abilitys[2].Fire();
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("R"))
        {
            if (abilitys[3] != null)
                abilitys[3].Fire();
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("D"))
        {
            if (abilitys[4] != null)
                abilitys[4].Fire();
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("D"))
        {
            if (abilitys[5] != null)
                abilitys[5].Fire();
        }



    }

    
}
