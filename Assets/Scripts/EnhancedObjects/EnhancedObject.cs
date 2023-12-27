using UnityEngine;

public abstract class EnhancedObject : MonoBehaviour
{
    [SerializeField] public UnitActionScriptableObject unitActionScriptableObject;
    public abstract void CreateAction();
}
