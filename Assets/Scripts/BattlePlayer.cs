using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattlePlayer : BasePlayer
{
    [SerializeField] private float stoppingDistance;
    [SerializeField] private PlayerAnimator playerAnimator;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public IEnumerator Move(Vector3 destination)
    {
        isWalking = true;
        navMeshAgent.SetDestination(destination);
        yield return new WaitForSeconds(0.01f);
        while (navMeshAgent.remainingDistance > stoppingDistance)
        {
            yield return new WaitForFixedUpdate();
        }

        navMeshAgent.isStopped = true;
        isWalking = false;
    }

    public void TakeDamage()
    {
        playerAnimator.TakeDamage();
    }

    public void Attack()
    {
        playerAnimator.RightMeleeAttack();
    }
}
