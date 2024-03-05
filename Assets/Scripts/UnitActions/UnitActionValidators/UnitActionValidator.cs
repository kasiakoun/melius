using UnityEngine;

public abstract class UnitActionValidator : MonoBehaviour
{
    public abstract bool CanAction(UnitActionParameters parameters);
}
