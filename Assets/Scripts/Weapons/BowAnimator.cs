using UnityEngine;

public class BowAnimator : MonoBehaviour
{
    private const string BOW_ACTION = "BowAction";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivateBow()
    {
        animator.SetTrigger(BOW_ACTION);
    }
}
