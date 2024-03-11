using UnityEngine;

public class AttackUnitActionFactory : UnitActionFactory
{
    public override IUnitAction CreateUnitAction(UnitActionParameters parameters)
    {
        if (!parameters.IsBuilded)
        {
            Debug.LogError("UnitActionParameters is not builded");
            return null;
        }

        if (parameters.Owner == null || parameters.Target == null)
        {
            Debug.LogError("Owner or target is null");
            return null;
        }

        return new AttackUnitAction(parameters.ScriptableObject, parameters.Owner, parameters.Target);
    }
}