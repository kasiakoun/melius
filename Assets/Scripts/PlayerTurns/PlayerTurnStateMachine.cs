using UnityEngine;

public abstract class PlayerTurnStateMachine : MonoBehaviour
{
    protected IBattlePlayerTurnState currentState;

    public EnemyTurnState enemyTurnState = new EnemyTurnState();
    public PlayerTurnState playerTurnState = new PlayerTurnState();
    public InitializeTurnState initializeTurnState = new InitializeTurnState();
    public VictoryTurnState victoryTurnState = new VictoryTurnState();
    public LostTurnState lostTurnState = new LostTurnState();

    public abstract BattleUI BattleUi { get; }

    private void Update()
    {
        currentState?.UpdateState();
    }

    public void ChangeState(IBattlePlayerTurnState state)
    {
        currentState?.OnExit();
        currentState = state;
        currentState.OnEnter(this);
    }

    public abstract void MakeVictory();
    public abstract void MakeLost();

}
