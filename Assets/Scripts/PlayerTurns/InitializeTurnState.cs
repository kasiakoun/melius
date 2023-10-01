public class InitializeTurnState : IBattlePlayerTurnState
{
    public void OnEnter(PlayerTurnStateMachine stateMachine)
    {
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
        else if (battlePlayerTurn.BattlePlayer is ComputerBattlePlayer)
        {
            stateMachine.ChangeState(stateMachine.enemyTurnState);
        }
    }

    public void UpdateState()
    {
    }
}
