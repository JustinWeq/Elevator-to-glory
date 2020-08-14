using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    public float Duration;
    public bool RegisteredBuff;
    protected GameObject owner;
    protected enemy_controller enemy_handle;
    protected unit_control_script unit_handle;
    protected bool on_enemy;
    protected float time_left;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        //set the duration
        time_left = Duration;
        on_enemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        //reduce time left
        time_left -= Time.deltaTime;
        BuffEffect();
        if(time_left <= 0)
        {
            if(RegisteredBuff)
            {
                if(on_enemy)
                {
                    enemy_handle.DeregisterBuff(this);
                }
                else
                {
                    unit_handle.DeregisterBuff(this);
                }
            }
            Destroy(gameObject);
        }
    }

    protected abstract void BuffEffect();

    public virtual string GetDescription()
    {
        return "";
    }

    public virtual Texture GetIcon()
    {
        return null;
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
        if((enemy_handle = owner.GetComponent<enemy_controller>()) != null)
        {
            on_enemy = true;
            if(RegisteredBuff)
            {
                enemy_handle.RegisterBuff(this);
            }
            return;
        }
        on_enemy = false;
        unit_handle = owner.GetComponent<unit_control_script>();
        if (RegisteredBuff)
        {
            unit_handle.RegisterBuff(this);
        }
    }
}
