using UnityEngine;

[RequireComponent(typeof(UnitHealth))]
public class UnitDamageable : MonoBehaviour
{
    [SerializeField] private UnitAnimator animator;
    [SerializeField] private UnitHealth unitHealth;

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
