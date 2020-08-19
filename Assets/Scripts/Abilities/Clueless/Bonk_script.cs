using Assets.Scripts.Buffs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonk_script :   Ability
{

    public float[] StunDuration = new float[6];
    public float[] DamageAmount = new float[6];

    private float damage;
    private float stun_duration;

    public AudioSource BonkSound;


    public override bool ActivateAbility(GameObject target)
    {
        if (remaining_cooldown <= 0.001f)
        {
            //activate the ability here

            //play the bonk sound

            BonkSound.Play();

            //apply a stun to the target
            enemy_controller enemy;
            unit_control_script unit;
            if ((enemy =target.GetComponent<enemy_controller>() )!= null)
            {
                Stun stun = GameObject.Instantiate(new GameObject()).AddComponent<Stun>();
                stun.SetDuration(stun_duration);
                stun.SetOwner(target);

                //face the owner towards the enemy
                unit_control_handle.FaceTowardsTarget(target.transform.position);
                //damage the enemy
                enemy.MagicDamage(damage, unit_control_handle);
            }

            

            remaining_cooldown = cooldown;

        }
        return true;
    }

    public override string GetDescription()
    {
        return "Stuns a single target for (" +stun_duration + ") and does " + damage + " magical damage";
    }

    protected override void LevelUp()
    {
        //level the ability
        damage = DamageAmount[GetLevel()-1];
        stun_duration = StunDuration[GetLevel()-1];
    }


}
