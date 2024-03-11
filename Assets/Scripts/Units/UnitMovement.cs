using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] public float stopDistance;

    private NavMeshAgent navMeshAgent;

    public float StopDistance => stopDistance;
    public bool IsWalking { get; private set; }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public IEnumerator Move(Vector3 destination, float stopDistance)
    {
        IsWalking = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(destination);
        yield return new WaitForSeconds(0.01f);
        while (navMeshAgent.remainingDistance > stopDistance)
        //while (navMeshAgent.hasPath)
        {
            Debug.Log($"Move: {navMeshAgent.remainingDistance}");
            yield return new WaitForFixedUpdate();
        }

        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(0.2f);
        IsWalking = false;
    }
}
