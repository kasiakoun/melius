using Microlight.MicroBar;
using UnityEngine;

[RequireComponent(typeof(MicroBar))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private UnitHealth unitHealth;
    private MicroBar microBar;

    private void Awake()
    {
        microBar = GetComponent<MicroBar>();
    }

    private void Start()
    {
        unitHealth.HealthChanged += OnHealthChanged;
        microBar.Initialize(unitHealth.MaxHealth);
    }

    private void OnDestroy()
    {
        unitHealth.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int val)
    {
        microBar.UpdateHealthBar(val);
    }

    public void Hide() => gameObject.SetActive(false);
}