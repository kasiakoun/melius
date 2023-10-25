using System;
using Microlight.MicroBar;
using UnityEngine;

[RequireComponent(typeof(UnitHealth))]
public class UnitDeath : MonoBehaviour
{
    private UnitHealth unitHealth;
    [SerializeField] private UnitAnimator unitAnimator;
    [SerializeField] private HealthBar healthBar;

    public event Action UnitDied;

    private void Awake()
    {
        unitHealth = GetComponent<UnitHealth>();
    }

    private void Start()
    {
        unitHealth.HealthIsOver += OnHealthIsOver;
    }

    private void OnDestroy()
    {
        unitHealth.HealthIsOver -= OnHealthIsOver;
    }

    private void OnHealthIsOver()
    {
        Die();
    }

    public void Die()
    {
        Debug.Log("Die");
        healthBar.Hide();
        unitAnimator.Death();

        UnitDied?.Invoke();
    }
}
