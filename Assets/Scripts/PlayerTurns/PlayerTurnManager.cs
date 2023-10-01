using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerTurnManager : PlayerTurnStateMachine
{
    [SerializeField] private BattlePlayer battlePlayer;
    [SerializeField] private ComputerBattlePlayer[] computerBattlePlayers;
    [SerializeField] private PlayerTurnEventsHandler playersTurnEventsHandler;
    [SerializeField] private BattleUI battleUi;

    private List<BattlePlayerTurn> currentRandomBattlePlayerTurns;

    public override BattleUI BattleUi => battleUi;

    private void Awake()
    {
        playersTurnEventsHandler.PlayerFinishActing += OnPlayerFinishActing;
    }

    private void OnDestroy()
    {
        playersTurnEventsHandler.PlayerFinishActing -= OnPlayerFinishActing;
    }

    private void Start()
    {
        currentState = initializeTurnState;
        NextTurn();
    }

    private void OnPlayerFinishActing(IBattleTurnPlayer battleTurnPlayer)
    {
        var battlePlayerTurn = currentRandomBattlePlayerTurns
            .FirstOrDefault(p => p.BattlePlayer == battleTurnPlayer && !p.TurnIsOver);
        if (battlePlayerTurn == null) return;

        battlePlayerTurn.TurnIsOver = true;
        NextTurn();
    }

    private void NextTurn()
    {
        if (currentRandomBattlePlayerTurns == null ||
            currentRandomBattlePlayerTurns.All(p => p.TurnIsOver))
        {
            currentRandomBattlePlayerTurns = CreateRandomBattlePlayerTurns();
        }

        var battlePlayerTurn = currentRandomBattlePlayerTurns.FirstOrDefault(p => !p.TurnIsOver);
        currentState.OnNextTurn(this, battlePlayerTurn);
    }

    private List<BattlePlayerTurn> CreateRandomBattlePlayerTurns()
    {
        var allAvailablePlayers = new List<IBattleTurnPlayer>(computerBattlePlayers);
        allAvailablePlayers.Insert(0, battlePlayer);

        var randomPlayers = new List<IBattleTurnPlayer>();
        var seed = (int)(DateTime.Now.Ticks % int.MaxValue);
        Random.InitState(seed);
        for (var i = allAvailablePlayers.Count - 1; i >= 0; i--)
        {
            var randomPlayerIndex = Random.Range(0, allAvailablePlayers.Count);
            var randomPlayer = allAvailablePlayers[randomPlayerIndex];

            randomPlayers.Add(randomPlayer);
            allAvailablePlayers.Remove(randomPlayer);
        }

        var battlePlayerTurns = randomPlayers.Select(p => new BattlePlayerTurn(p)).ToList();
        return battlePlayerTurns;
    }
}
