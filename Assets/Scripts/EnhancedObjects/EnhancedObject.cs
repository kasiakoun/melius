using System.Collections;
using UnityEngine;

public abstract class EnhancedObject : MonoBehaviour
{
    [SerializeField] public UnitActionScriptableObject unitActionScriptableObject;
    public abstract void ActivateObject();
    public abstract void DeactivateObject();
}
