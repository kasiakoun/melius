public class VictoryTurnState : IBattlePlayerTurnState
{
    public void OnEnter(PlayerTurnStateMachine stateMachine)
    {
        stateMachine.MakeVictory();
    }

    public void OnExit()
    {
    }

    public void OnNextTurn(PlayerTurnStateMachine stateMachine, BattlePlayerTurn battlePlayerTurn)
    {

    }

    public void UpdateState()
    {
    }
}
