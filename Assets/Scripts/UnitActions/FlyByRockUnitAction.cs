using System.Collections;
using UnityEngine;

public class FlyByRockUnitAction : UnitActionWithStatusEffect<FlyingRockUnitStatusEffectSO>
{
    private readonly BattleUnitBase unit;
    private readonly EnhancedObject enhancedObject;

    public FlyByRockUnitAction(UnitActionScriptableObject scriptableObject,
        BattleUnitBase unit,
        EnhancedObject enhancedObject)
        : base(scriptableObject)
    {
        this.unit = unit;
        this.enhancedObject = enhancedObject;
    }

    public override IEnumerator MakeAction()
    {
        if (unit is not IFlyingUnit flyingUnit)
        {
            Debug.LogError("IFlyingUnit is not implemented");
            yield break;
        }

        yield return unit.Move(enhancedObject.transform.position);
        enhancedObject.transform.parent = flyingUnit.BaseHolder;
        enhancedObject.ActivateObject();
        yield return unit.ApplyEffect(UnitStatusEffectSO);
        enhancedObject.DeactivateObject();
    }
}
