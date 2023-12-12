using UnityEngine;

[RequireComponent(typeof(UnitHealth))]
public class UnitDamageable : MonoBehaviour
{
    [SerializeField] private BaseUnitAnimator animator;
    [SerializeField] private Transform target;
    private UnitHealth unitHealth;

    public Transform Target => target;

    private void Awake()
    {
        unitHealth = GetComponent<UnitHealth>();
    }

    public void TakeDamage()
    {
        animator.TakeDamage();
        // todo: replace with dynamic damage
        unitHealth.Damage(5);
    }
}
