using System.Collections;
using UnityEngine;

public abstract class EnhancedObject : MonoBehaviour
{
    [SerializeField] public UnitActionComposite unitActionComposite;
    public abstract void ActivateObject();
    public abstract void DeactivateObject();
}
