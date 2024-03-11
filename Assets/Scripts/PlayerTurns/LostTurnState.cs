public class LostTurnState : IBattlePlayerTurnState
{
    public void OnEnter(PlayerTurnStateMachine stateMachine)
    {
        stateMachine.MakeLost();
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
