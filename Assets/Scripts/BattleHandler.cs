using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform enemy;

    public void Handle()
    {
        StartCoroutine(StartHandeling(player, enemy));
    }

    private IEnumerator StartHandeling(Transform player, Transform enemy)
    {
        //var battlePlayer = player.GetComponent<BattlePlayer>();
        //var battleUnit = enemy.GetComponent<BattleUnit>();
        //var unitNavMeshAgent = enemy.GetComponent<NavMeshAgent>();
        //var playerNavMeshAgent = player.GetComponent<NavMeshAgent>();

        //var destination = unitNavMeshAgent.destination;

        //yield return battlePlayer.Move(destination);
        //battlePlayer.Attack();
        var delayBeforeTakingDamage = 0.6f;
        yield return new WaitForSeconds(delayBeforeTakingDamage);
        //battleUnit.TakeDamage();

        //yield return new WaitForSeconds(1.0f);

        //destination = playerNavMeshAgent.destination;

        //yield return battleUnit.Move(destination);
        //yield return battleUnit.Rotate(battlePlayer.transform);
        //battleUnit.Attack();
        //delayBeforeTakingDamage = 0.9f;
        //yield return new WaitForSeconds(delayBeforeTakingDamage);
        //battlePlayer.TakeDamage();
    }
}
