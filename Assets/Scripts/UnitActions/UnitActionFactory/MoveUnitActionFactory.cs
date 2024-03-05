using UnityEngine;

public class MoveUnitActionFactory : UnitActionFactory
{
    public override IUnitAction CreateUnitAction(UnitActionParameters parameters)
    {
        if (parameters.Owner == null || parameters.Position == null)
        {
            Debug.LogError("Owner or Position is null");
            return null;
        }

        return new MoveUnitAction(parameters.ScriptableObject, parameters.Owner, parameters.Position.Value);
    }
}
