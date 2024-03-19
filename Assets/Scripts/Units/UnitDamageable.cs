using UnityEngine;

[RequireComponent(typeof(UnitHealth))]
public class UnitDamageable : MonoBehaviour
{
    [SerializeField] private BaseUnitAnimator animator;
    [SerializeField] private Transform target;
    private UnitHealth unitHealth;
    private UnitDamageText unitDamageText;

    public Transform Target => target;

    private void Awake()
    {
        unitHealth = GetComponent<UnitHealth>();
        unitDamageText = GetComponent<UnitDamageText>();
    }

    public void TakeDamage()
    {
        animator.TakeDamage();
        // todo: replace with dynamic damage
        var damageValue = 5;
        unitHealth.Damage(damageValue);
        unitDamageText.Show(damageValue);
    }
}
