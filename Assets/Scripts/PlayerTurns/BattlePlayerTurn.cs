public class BattlePlayerTurn
{
    public IBattleTurnPlayer BattlePlayer { get; private set; }
    public bool TurnIsOver { get; set; }

    public BattlePlayerTurn(IBattleTurnPlayer battlePlayer)
    {
        BattlePlayer = battlePlayer;
    }
}
