using UnityEngine.UI;

public interface IBattleTurnPlayer
{
    bool UnitIsDead { get; }
    IBattleUnit BattleUnit { get; }
    void MakeTurn();
}
