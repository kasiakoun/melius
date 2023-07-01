using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] private UnitAnimator unitAnimator;
    [SerializeField] private float stoppingDistance;

    private NavMeshAgent navMeshAgent;
    private bool isWalking;

    public void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public bool IsWalking() => isWalking;

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
        unitAnimator.TakeDamage();
    }

    public void Attack()
    {
        unitAnimator.Attack();
    }
}
