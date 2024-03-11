using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private PickedActionsPanel pickedActionsPanel;
    [SerializeField] private PlayerTurnEventsHandler turnEventsHandler;

    public void Handle(IBattleTurnPlayer battleTurnPlayer, List<IUnitAction> unitActions)
    {
        StartCoroutine(StartHandling(battleTurnPlayer, unitActions));
    }

    private IEnumerator StartHandling(IBattleTurnPlayer battleTurnPlayer, List<IUnitAction> unitActions)
    {
        turnEventsHandler.OnPlayerStartActing(battleTurnPlayer);
        foreach (var unitAction in unitActions)
        {
            yield return unitAction.MakeAction();
        }

        turnEventsHandler.OnPlayerFinishActing(battleTurnPlayer);

        pickedActionsPanel.ClearPanel();
    }
}
