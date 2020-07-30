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
       // MoveTo(new Vector3(50, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTo(Vector3 position)
    {
        navmeshAgent.destination = position;
    }
}
