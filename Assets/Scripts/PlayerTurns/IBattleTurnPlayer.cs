using UnityEngine.UI;

public interface IBattleTurnPlayer
{
    bool UnitIsDead { get; }
    BattleUnitBase BattleUnit { get; }
    void MakeTurn();
}
