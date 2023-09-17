using UnityEngine;

public class UnitDamageable : MonoBehaviour
{
    [SerializeField] private UnitAnimator animator;

    public void TakeDamage()
    {
        animator.TakeDamage();
    }
}
