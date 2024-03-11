using UnityEngine;

public class UnitActionComposite : MonoBehaviour
{
    [SerializeField] public UnitActionFactory factory;
    [SerializeField] public UnitActionValidator validator;
    [SerializeField] public UnitActionScriptableObject scriptableObject;
    [SerializeField] public WaitingPicker picker;
}
