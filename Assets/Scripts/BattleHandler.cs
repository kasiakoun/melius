using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public void Handle(List<IUnitAction> unitActions)
    {
        StartCoroutine(StartHandling(unitActions));
    }

    private IEnumerator StartHandling(List<IUnitAction> unitActions)
    {
        foreach (var unitAction in unitActions)
        {
            yield return unitAction.MakeAction();
        }
    }
}
