using UnityEngine;

public class FlyRockUnitActionFactory : UnitActionFactory
{
    public override IUnitAction CreateUnitAction(UnitActionParameters parameters)
    {
        if (parameters.Owner == null || parameters.EnhancedObject == null)
        {
            Debug.LogError("Owner or EnhancedObject is null");
            return null;
        }

        return new FlyByRockUnitAction(parameters.ScriptableObject, parameters.Owner, parameters.EnhancedObject);
    }
}