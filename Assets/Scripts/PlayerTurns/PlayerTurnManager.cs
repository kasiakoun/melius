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
    [SerializeField] private GameObject victoryText;
    [SerializeField] private GameObject lostText;

    private List<BattlePlayerTurn> currentRandomBattlePlayerTurns;
    private List<BattlePlayerTurn> nextRandomBattlePlayerTurns;
    private List<BattlePlayerTurn> playerTurns = new List<BattlePlayerTurn>();

    public event Action<List<BattlePlayerTurn>> PlayerTurnsChanged;
    public event Action<BattlePlayerTurn> TurnChanged;

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
        ChangeState(initializeTurnState);
        NextTurn();
    }

    private void OnPlayerFinishActing(IBattleTurnPlayer battleTurnPlayer)
    {
        var battlePlayerTurn = currentRandomBattlePlayerTurns
            .FirstOrDefault(p => p.BattlePlayer == battleTurnPlayer && !p.TurnIsOver && !p.BattlePlayer.UnitIsDead);
        if (battlePlayerTurn == null) return;

        battlePlayerTurn.TurnIsOver = true;
        NextTurn();
    }

    private void NextTurn()
    {
        TryToSetupBattlePlayerTurns();

        var oneUnitIsAlive = currentRandomBattlePlayerTurns
            .Where(p => !p.BattlePlayer.UnitIsDead)
            .Select(p => p.BattlePlayer)
            .Distinct()
            .Count() == 1;
        var battlePlayerTurn = oneUnitIsAlive
            ? null
            : currentRandomBattlePlayerTurns.FirstOrDefault(p => !p.TurnIsOver && !p.BattlePlayer.UnitIsDead);
        currentState.OnNextTurn(this, battlePlayerTurn);
        if (battlePlayerTurn == null) return;
        TurnChanged?.Invoke(battlePlayerTurn);
    }

    private void TryToSetupBattlePlayerTurns()
    {
        if (currentRandomBattlePlayerTurns != null &&
            !currentRandomBattlePlayerTurns.All(p => p.TurnIsOver)) return;

        currentRandomBattlePlayerTurns = nextRandomBattlePlayerTurns ?? CreateRandomBattlePlayerTurns();
        nextRandomBattlePlayerTurns = CreateRandomBattlePlayerTurns();

        playerTurns.AddRange(currentRandomBattlePlayerTurns);
        PlayerTurnsChanged?.Invoke(playerTurns);
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

    public override BattleUI BattleUi => battleUi;
    public override void MakeVictory()
    {
        BattleUi.Hide();
        victoryText.SetActive(true);
    }

    public override void MakeLost()
    {
        BattleUi.Hide();
        lostText.SetActive(true);
    }
}
