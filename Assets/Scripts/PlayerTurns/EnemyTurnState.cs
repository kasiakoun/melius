public class EnemyTurnState : IBattlePlayerTurnState
{
    public void OnEnter(PlayerTurnStateMachine stateMachine)
    {
        stateMachine.BattleUi.Hide();
    }

    public void OnExit()
    {
    }

    public void OnNextTurn(PlayerTurnStateMachine stateMachine, BattlePlayerTurn battlePlayerTurn)
    {
        if (battlePlayerTurn.BattlePlayer is BattlePlayer)
        {
            stateMachine.ChangeState(stateMachine.playerTurnState);
        }

        battlePlayerTurn.BattlePlayer.MakeTurn();
    }

    public void UpdateState()
    {
    }
}
