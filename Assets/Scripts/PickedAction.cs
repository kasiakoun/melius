using UnityEngine;
using UnityEngine.UI;

public class PickedAction : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Image actionImage;
    [SerializeField] private Image targetImage;

    public bool IsCleared { get; private set; } = true;
    public IUnitAction UnitAction { get; private set; }

    public void SetupAction(IUnitAction unitAction)
    {
        // todo: replace in the future with specific actions
        var attackUnitAction = unitAction as AttackUnitAction;
        actionImage.sprite = attackUnitAction.ScriptableObject.icon;
        targetImage.sprite = attackUnitAction.TargetUnit.ScriptableObject.icon;
        target.SetActive(true);
        actionImage.gameObject.SetActive(true);

        IsCleared = false;
        UnitAction = unitAction;
    }

    public void ClearAction()
    {
        target.SetActive(false);
        actionImage.gameObject.SetActive(false);
        actionImage.sprite = null;
        targetImage.sprite = null;

        IsCleared = true;
        UnitAction = null;
    }
}
