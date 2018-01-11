using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombi_move : MonoBehaviour
{

    [SerializeField]
    GameObject[] targetNavMeshObjects;

    int targetNavMeshObjectCounts;
    int targetNavMeshObjectNow = 0;
    NavMeshAgent navMeshAgentCompornent;

    // Use this for initialization
    void Start()
    {

        navMeshAgentCompornent = this.GetComponent<NavMeshAgent>();
        targetNavMeshObjectCounts = targetNavMeshObjects.Length - 1;

    }

    // Update is called once per frame
    void Update()
    {

        if (navMeshAgentCompornent.remainingDistance < 0.1f)
        {

            if (targetNavMeshObjectNow <= targetNavMeshObjectCounts)
            {
                navMeshAgentCompornent.SetDestination(targetNavMeshObjects[targetNavMeshObjectNow].transform.position);
            }
            else if (targetNavMeshObjectNow > targetNavMeshObjectCounts)
            {
                targetNavMeshObjectNow = 0;
                navMeshAgentCompornent.SetDestination(targetNavMeshObjects[targetNavMeshObjectNow].transform.position);
            }
            targetNavMeshObjectNow++;
        }
    }
}


