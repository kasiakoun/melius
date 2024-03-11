using UnityEngine;

public abstract class WaitingPicker : MonoBehaviour
{
    public abstract void StartPicking(UnitActionValidator validator, BattleUnitBase battleUnit);
}