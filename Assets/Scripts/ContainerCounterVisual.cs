using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string IS_OPEN_CLOSED = "IsOpenClosed";

    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        containerCounter.PlayerGrabbedObject += OnPlayerGrabbedObject;
    }

    private void OnPlayerGrabbedObject()
    {
        animator.SetTrigger(IS_OPEN_CLOSED);
    }
}
