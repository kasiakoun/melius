using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    [SerializeField] private BattlePlayer battlePlayer;
    [SerializeField] private ComputerBattlePlayer[] computerBattlePlayers;
    [SerializeField] private PlayerTurnEventsHandler playersTurnEventsHandler;
    [SerializeField] private BattleUI battleUi;

    private List<BattlePlayerTurn> currentRandomBattlePlayerTurns;
    private BattlePlayerTurn currentBattlePlayerTurn;

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
        if (currentBattlePlayerTurn?.BattlePlayer == battlePlayerTurn.BattlePlayer)
        {

        }
        else if (battlePlayerTurn.BattlePlayer is BattlePlayer)
        {
            battleUi.Show();
        }
        else if (battlePlayerTurn.BattlePlayer is ComputerBattlePlayer)
        {
            battleUi.Hide();
        }

        battlePlayerTurn.BattlePlayer.MakeTurn();
        currentBattlePlayerTurn = battlePlayerTurn;
    }

    private List<BattlePlayerTurn> CreateRandomBattlePlayerTurns()
    {
        var allAvailablePlayers = new List<IBattleTurnPlayer>(computerBattlePlayers);
        allAvailablePlayers.Insert(0, battlePlayer);

        var randomPlayers = new List<IBattleTurnPlayer>();
        for (var i = allAvailablePlayers.Count - 1; i >= 0; i--)
        {
            var randomPlayerIndex = Random.Range(0, allAvailablePlayers.Count - 1);
            var randomPlayer = allAvailablePlayers[randomPlayerIndex];

            randomPlayers.Add(randomPlayer);
            allAvailablePlayers.Remove(randomPlayer);
        }

        var battlePlayerTurns = randomPlayers.Select(p => new BattlePlayerTurn(p)).ToList();
        return battlePlayerTurns;
    }
}
