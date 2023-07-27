using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattlePlayer : MonoBehaviour, IBattleUnit
{
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float rotateSpeed = 10.0f;
    [SerializeField] private float delayBeforeHit;
    [SerializeField] private UnitScriptableObject scriptableObject;

    private NavMeshAgent navMeshAgent;
    private Outline outline;

    private bool isWalking;

    public void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        outline = GetComponent<Outline>();
    }

    public bool IsWalking() => isWalking;

    #region IBattleUnit Implementation

    public Vector3 Position => navMeshAgent.destination;
    public UnitScriptableObject ScriptableObject => scriptableObject;

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

    public IEnumerator Rotate(Vector3 position)
    {
        var targetRotation = Quaternion.LookRotation(position - transform.position);

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

    public IEnumerator Attack()
    {
        playerAnimator.RightMeleeAttack();
        yield return new WaitForSeconds(delayBeforeHit);
    }

    public void SetHighlightOutline(bool enable)
    {
        outline.enabled = enable;
    }

    #endregion
}
