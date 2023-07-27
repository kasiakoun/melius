using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickedAction : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Image actionImage;
    [SerializeField] private Image targetImage;

    public bool IsCleared { get; private set; } = true;

    // todo: replace with common class / interface
    public void SetupAction(AttackUnitAction unitAction)
    {
        actionImage.sprite = unitAction.ScriptableObject.icon;
        targetImage.sprite = unitAction.TargetUnit.ScriptableObject.icon;
        target.SetActive(true);
        actionImage.gameObject.SetActive(true);

        IsCleared = false;
    }

    public void ClearAction()
    {
        target.SetActive(false);
        actionImage.gameObject.SetActive(false);
        actionImage.sprite = null;
        targetImage.sprite = null;

        IsCleared = true;
    }
}
