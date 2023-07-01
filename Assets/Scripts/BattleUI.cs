using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private BattleHandler battleHandler;

    public void AttackClicked()
    {
        battleHandler.Handle();
        Debug.Log("AttackClicked");
    }
}
