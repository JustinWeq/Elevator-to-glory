using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    public bool Purgeable;
    public bool Positive;
    protected GameObject owner;
    protected enemy_controller enemy_handle;
    protected unit_control_script unit_handle;
    protected bool on_enemy;
    protected float time_left;
    protected float start_time;

    // Start is called before the first frame update
    private void Start()
    {


    }

    public void SetDuration(float duration)
    {
        time_left = duration;
        start_time = time_left;
    }

    // Update is called once per frame
    void Update()
    {
        //reduce time left
        time_left -= Time.deltaTime;
        if(owner == null)
        {
            Destroy(gameObject);
        }
        BuffEffect();
        if(time_left <= 0)
        {

            if (on_enemy)
            {
                if(enemy_handle != null)
                enemy_handle.DeregisterBuff(this);
            }
            else
            {
                if(unit_handle != null)
                unit_handle.DeregisterBuff(this);

            }
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (owner == null)
            return;
        if(on_enemy)
        {
            if (enemy_handle == null)
                return;
            enemy_handle.DeregisterBuff(this);
            return;
        }
        if (unit_handle == null)
            return;
        unit_handle.DeregisterBuff(this);
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
            enemy_handle.RegisterBuff(this);
            return;
        }
        on_enemy = false;
        unit_handle = owner.GetComponent<unit_control_script>();
        unit_handle.RegisterBuff(this);
    }
}
