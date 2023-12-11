using UnityEngine;

[RequireComponent(typeof(UnitHealth))]
public class UnitPlayerDamageable : MonoBehaviour
{
    [SerializeField] private PlayerAnimator animator;
    [SerializeField] private Transform target;
    private UnitHealth unitHealth;

    private void Awake()
    {
        unitHealth = GetComponent<UnitHealth>();
    }

    public void TakeDamage()
    {
        animator.TakeDamage();
        // todo: replace with dynamic damage
        unitHealth.Damage(10);
    }
}
