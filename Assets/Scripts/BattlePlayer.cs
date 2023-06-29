using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattlePlayer : BasePlayer
{
    [SerializeField] private float stoppingDistance;
    [SerializeField] private NavMeshAgent navMeshAgent;

    public IEnumerator Move(Vector3 destination)
    {
        isWalking = true;
        Debug.Log("isWalking = true");
        navMeshAgent.SetDestination(destination);
        yield return new WaitForSeconds(0.05f);
        while (navMeshAgent.remainingDistance > stoppingDistance)
        {
            yield return new WaitForFixedUpdate();
        }

        navMeshAgent.isStopped = true;
        Debug.Log("isWalking = false");
        isWalking = false;
    }
}
