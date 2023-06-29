using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattlePlayer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    public void Move(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
}
