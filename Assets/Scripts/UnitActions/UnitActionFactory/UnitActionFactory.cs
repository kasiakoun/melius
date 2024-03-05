using UnityEngine;

public abstract class UnitActionFactory : MonoBehaviour
{
    public abstract IUnitAction CreateUnitAction(UnitActionParameters parameters);
}
