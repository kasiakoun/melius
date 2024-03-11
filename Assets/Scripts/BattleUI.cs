using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private BattleHandler battleHandler;
    [SerializeField] private QuickCastPanel quickCastPanel;
    [SerializeField] private PickedActionsPanel pickedActionsPanel;

    public void Hide()
    {
        quickCastPanel.HidePanel();
        pickedActionsPanel.HidePanel();
    }

    public void Show()
    {
        quickCastPanel.ShowPanel();
        pickedActionsPanel.ShowPanel();
    }
}
