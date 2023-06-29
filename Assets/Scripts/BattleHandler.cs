using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform enemy;

    public void Attack()
    {
        var battlePlayer = player.GetComponent<BattlePlayer>();
        var unitNavMeshAgent = enemy.GetComponent<NavMeshAgent>();

        var destination = unitNavMeshAgent.destination;

        StartCoroutine(battlePlayer.Move(destination));
    }
}
