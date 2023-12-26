using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenPointPicker : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;

    [SerializeField] private PositionPicker positionPicker;
    [SerializeField] private UnitPicker unitPicker;
    [SerializeField] private ObjectPicker objectPicker;

    public void Start()
    {
        gameInput.MouseLeftClicked += OnMouseLeftClicked;
    }

    private void OnMouseLeftClicked(Vector3 vector)
    {
        if (unitPicker.HandleLeftClick(vector)) return;
        if (objectPicker.HandleLeftClick(vector)) return;
        if (positionPicker.HandleLeftClick(vector)) return;
    }
}
