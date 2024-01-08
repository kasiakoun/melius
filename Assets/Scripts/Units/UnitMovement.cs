using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float stoppingDistance;

    private NavMeshAgent navMeshAgent;

    public bool IsWalking { get; private set; }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public IEnumerator Move(Vector3 destination)
    {
        IsWalking = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(destination);
        yield return new WaitForSeconds(0.01f);
        while (navMeshAgent.remainingDistance > 0.1)
        // todo: in the future we have to reload Move method with
        // todo: distance paramter to have more accurate position
        //while (navMeshAgent.hasPath)
        {
            yield return new WaitForFixedUpdate();
        }

        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(0.2f);
        IsWalking = false;
    }
}
