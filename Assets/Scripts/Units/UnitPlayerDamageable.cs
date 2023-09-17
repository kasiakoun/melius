using UnityEngine;

public class UnitPlayerDamageable : MonoBehaviour
{
    [SerializeField] private PlayerAnimator animator;

    public void TakeDamage()
    {
        animator.TakeDamage();
    }
}
