using UnityEditorInternal;

public interface IBattlePlayerTurnState
{
    public void OnEnter(PlayerTurnStateMachine stateMachine);
    public void OnExit();
    public void OnNextTurn(PlayerTurnStateMachine stateMachine, BattlePlayerTurn battlePlayerTurn);
    public void UpdateState();
}
