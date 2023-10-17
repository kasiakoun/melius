using System;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    private int health;
    private int maxHealth;

    public int MaxHealth => maxHealth;

    public event Action<int> HealthChanged;
    public event Action HealthIsOver;

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
        HealthChanged?.Invoke(health);
    }

    public void Damage(int damage)
    {
        var val = health - damage;
        health = val <= 0 ? 0 : val;
        HealthChanged?.Invoke(health);

        if (health == 0)
        {
            HealthIsOver?.Invoke();
        }
    }
}
