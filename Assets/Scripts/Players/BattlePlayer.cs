using UnityEngine;

public class BattlePlayer : MonoBehaviour, IBattleTurnPlayer
{
    [SerializeField] private PlayerBattleUnit battleUnit;

    #region IBattlePlayerTurn Implementation

    public void MakeTurn()
    {
        //battleUI.Show();
    }

    #endregion
}
