using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class unit_move_script : MonoBehaviour
{
    NavMeshAgent navmeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        //locate the navMeshAgent
        navmeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveTo(Vector3 position)
    {
        navmeshAgent.destination = position;
    }
}
