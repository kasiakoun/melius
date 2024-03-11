using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEffects : MonoBehaviour
{
    [SerializeField] private PlayerTurnManager playerTurnManager;

    private BattleUnitBase battleUnit;

    public List<UnitStatusEffect> ActiveUnitStatusEffects { get; } = new();

    private void Awake()
    {
        battleUnit = GetComponent<BattleUnitBase>();
        playerTurnManager.RoundChanged += OnRoundChanged;
    }

    private void OnDestroy()
    {
        playerTurnManager.RoundChanged -= OnRoundChanged;
    }

    private void OnRoundChanged()
    {
        StartCoroutine(UpdateEffects());
    }

    public IEnumerator ApplyEffect(UnitStatusEffectSO effect)
    {
        var unitStatusEffect = new UnitStatusEffect(effect);

        yield return unitStatusEffect.UnitStatusEffectSO.ApplyEffect(battleUnit);
        ActiveUnitStatusEffects.Add(unitStatusEffect);
    }

    private IEnumerator UpdateEffects()
    {
        for (var i = ActiveUnitStatusEffects.Count - 1; i >= 0; i--)
        {
            var statusEffect = ActiveUnitStatusEffects[i];
            statusEffect.DecreaseDuration();
            if (statusEffect.DurationLeft <= 0)
            {
                yield return statusEffect.UnitStatusEffectSO.RemoveEffect(battleUnit);
                ActiveUnitStatusEffects.Remove(statusEffect);
            }
        }
    }
}
