using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitFlying : MonoBehaviour
{
    [SerializeField] private Transform baseHolder;
    private NavMeshAgent navMeshAgent;

    private float moveSpeed = 3f;
    private float targetHeight = 4f;

    private float levitateHeight = 0.33f;
    private float levitateSpeed = 3f;

    private bool levitatedIsStarted;
    private float startBaseOffset;

    private float initialBaseOffset;

    public Transform BaseHolder => baseHolder;

    public bool IsFlying { get; private set; }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialBaseOffset = navMeshAgent.baseOffset;
    }

    private void Update()
    {
        if (!levitatedIsStarted) return;

        var yOffset = Mathf.Sin(Time.time * levitateSpeed) * levitateHeight;

        navMeshAgent.baseOffset = startBaseOffset + yOffset;
    }

    public IEnumerator ActivateObject()
    {
        IsFlying = true;
        var currentBaseOffset = navMeshAgent.baseOffset;

        while (currentBaseOffset < targetHeight)
        {
            Debug.Log($"ActivateObject: {currentBaseOffset}");
            var nextPosition = currentBaseOffset + moveSpeed * Time.deltaTime;
            navMeshAgent.baseOffset = nextPosition;
            currentBaseOffset = navMeshAgent.baseOffset;
            yield return new WaitForEndOfFrame();
        }

        startBaseOffset = navMeshAgent.baseOffset;
        levitatedIsStarted = true;
    }

    public IEnumerator DeactivateObject()
    {
        IsFlying = false;
        levitatedIsStarted = false;
        var currentBaseOffset = navMeshAgent.baseOffset;

        while (currentBaseOffset > initialBaseOffset)
        {
            Debug.Log($"ActivateObject: {currentBaseOffset}");
            var nextPosition = currentBaseOffset - moveSpeed * Time.deltaTime;
            navMeshAgent.baseOffset = nextPosition;
            currentBaseOffset = navMeshAgent.baseOffset;
            yield return new WaitForEndOfFrame();
        }

        navMeshAgent.baseOffset = initialBaseOffset;
    }
}
