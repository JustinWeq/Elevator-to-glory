using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class unit_move_script : MonoBehaviour
{
    NavMeshAgent navmeshAgent;
    unit_control_script unit;
    // Start is called before the first frame update
    void Start()
    {
        //locate the navMeshAgent
        navmeshAgent = GetComponent<NavMeshAgent>();
        //get the unit
        unit = GetComponent<unit_control_script>();
        // MoveTo(new Vector3(50, 0, 0));

        //disable rotation of the nav mesh
        navmeshAgent.updateRotation = false;
        navmeshAgent.angularSpeed = 0;
        //set the stopping distance to be the same as the obstacle avoidance radius
        navmeshAgent.stoppingDistance = 30;//navmeshAgent.radius*2;
    }

    // Update is called once per frame
    void Update()
    {
        if (unit.GetCanMove())
        {
            //set the speed on the nav mesh agent to that of the unit
            navmeshAgent.speed = (unit.GetMovespeed() + unit.GetAddedMovespeed()) / 10;
            navmeshAgent.acceleration = unit.GetMovespeed() + unit.GetAddedMovespeed();
        }
        else
        {
            navmeshAgent.speed = 0;
            navmeshAgent.acceleration = 0;
        }
        
    }

    public void CorrectRotation()
    {
        if (navmeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
            transform.rotation = Quaternion.LookRotation(navmeshAgent.velocity.normalized);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    public void MoveTo(Vector3 position,float stopping_distance = 0)
    {
        navmeshAgent.destination = position;
        navmeshAgent.stoppingDistance = stopping_distance;
    }
}
