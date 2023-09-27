using UnityEngine;

public class ComputerBattlePlayer : MonoBehaviour, IBattleTurnPlayer
{
    [SerializeField] private BattleUnit battleUnit;

    #region IBattlePlayerTurn Implementation

    public void MakeTurn()
    {

    }

    #endregion
}
