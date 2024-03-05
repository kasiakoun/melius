using System.Collections;
using UnityEngine;

public class MoveUnitAction : UnitAction
{
    private BattleUnitBase owner;
    private Vector3 targetPosition;

    public MoveUnitAction(UnitActionScriptableObject scriptableObject,
        BattleUnitBase owner,
        Vector3 targetPosition)
        : base(scriptableObject)
    {
        this.owner = owner;
        this.targetPosition = targetPosition;
    }

    public override IEnumerator MakeAction()
    {
        yield return owner.Move(targetPosition);
    }
}
