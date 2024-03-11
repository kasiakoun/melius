using System.Collections;
using UnityEngine;

public class FlyingRockUnitStatusEffectSO : UnitStatusEffectSO
{
    public override int Duration => 1;

    public override IEnumerator ApplyEffect(BattleUnitBase unit)
    {
        Debug.Log("Effect was added");
        if (unit is not IFlyingUnit flyingUnit)
        {
            Debug.LogError($"unit is not {nameof(IFlyingUnit)}");
            yield break;
        }

        yield return flyingUnit.ActivateFlying();
    }

    public override IEnumerator RemoveEffect(BattleUnitBase unit)
    {
        Debug.Log("Effect was removed");
        if (unit is not IFlyingUnit flyingUnit)
        {
            Debug.LogError($"unit is not {nameof(IFlyingUnit)}");
            yield break;
        }

        yield return flyingUnit.DeactivateFlying();
        flyingUnit.BaseHolder.DetachChildren();
    }
}