using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattlePlayer : BasePlayer, IBattleUnit
{
    [SerializeField] private float stoppingDistance;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private float rotateSpeed = 10.0f;

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

    public IEnumerator Rotate(Transform unit)
    {
        var targetRotation = Quaternion.LookRotation(unit.position - transform.position);

        while (Mathf.Abs(Quaternion.Angle(targetRotation, transform.rotation)) > 1.0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
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
