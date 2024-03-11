using UnityEngine;

public class AttackUnitActionValidator : UnitActionValidator
{
    public override bool CanAction(UnitActionParameters parameters)
    {
        if (parameters.Owner == null || parameters.Target == null)
        {
            Debug.LogError("Owner or Target is null");
            return false;
        }

        var flyingUnit = parameters.Target as IFlyingUnit;
        if (flyingUnit != null && 
            flyingUnit.IsFlying &&
            parameters.Owner.IsMelee())
        {
            return false;
        }

        return true;
    }
}
