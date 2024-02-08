using System.Collections.Generic;
using UnityEngine;

public class UnitEffects : MonoBehaviour
{
    public List<UnitStatusEffect> ActiveUnitStatusEffects { get; } = new();

    public void ApplyEffect(UnitStatusEffectSO effect)
    {
        var unitStatusEffect = new UnitStatusEffect(effect);
        ActiveUnitStatusEffects.Add(unitStatusEffect);
    }

    public void UpdateEffects()
    {
        foreach (var statusEffect in ActiveUnitStatusEffects)
        {
            statusEffect.DecreaseDuration();
            if (statusEffect.DurationLeft <= 0)
            {
                ActiveUnitStatusEffects.Remove(statusEffect);
            }
        }
    }
}
