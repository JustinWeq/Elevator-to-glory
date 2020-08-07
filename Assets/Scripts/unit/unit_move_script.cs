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
        //navmeshAgent.
    }

    // Update is called once per frame
    void Update()
    {
        //set the speed on the nav mesh agent to that of the unit
        navmeshAgent.speed = (unit.GetMovespeed() + unit.GetAddedMovespeed())/10;
        navmeshAgent.acceleration = unit.GetMovespeed() + unit.GetAddedMovespeed();
        
    }

    void LateUpdate()
    {
        if(navmeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
            transform.rotation = Quaternion.LookRotation(navmeshAgent.velocity.normalized);
    }

    public void MoveTo(Vector3 position)
    {
        navmeshAgent.destination = position;
    }
}
