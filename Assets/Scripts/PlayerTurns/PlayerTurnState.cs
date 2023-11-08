public class PlayerTurnState : IBattlePlayerTurnState
{
    public void OnEnter(PlayerTurnStateMachine stateMachine)
    {
        stateMachine.BattleUi.Show();
    }

    public void OnExit()
    {
    }

    public void OnNextTurn(PlayerTurnStateMachine stateMachine, BattlePlayerTurn battlePlayerTurn)
    {
        if (battlePlayerTurn == null)
        {
            stateMachine.ChangeState(stateMachine.victoryTurnState);
        }
        else if (battlePlayerTurn.BattlePlayer is ComputerBattlePlayer)
        {
            stateMachine.ChangeState(stateMachine.enemyTurnState);
        }

        battlePlayerTurn?.BattlePlayer.MakeTurn();
    }

    public void UpdateState()
    {
    }
}
