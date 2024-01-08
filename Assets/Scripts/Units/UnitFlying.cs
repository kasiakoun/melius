using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitFlying : MonoBehaviour
{
    [SerializeField] private Transform baseHolder;
    private NavMeshAgent navMeshAgent;

    private float moveSpeed = 5f;
    private float targetHeight = 4f;

    private float levitateHeight = 0.33f;
    private float levitateSpeed = 2f;

    private bool levitatedIsStarted;
    private float startBaseOffset;

    public Transform BaseHolder => baseHolder;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!levitatedIsStarted) return;

        var yOffset = Mathf.Sin(Time.time * levitateSpeed) * levitateHeight;

        navMeshAgent.baseOffset = startBaseOffset + yOffset;
    }

    public IEnumerator ActivateObject()
    {
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
}
