using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTurnsPanel : MonoBehaviour
{
    [SerializeField] private PlayerTurnManager playerTurnManager;
    [SerializeField] private GameObject playerTurns;
    [SerializeField] private Transform playerTurnPrefab;

    private List<(BattlePlayerTurn BattlePlayerTurn, PlayerTurn PlayerTurn)> _turnCollection = new();
    private PlayerTurn _lastPlayerTurn;

    private void Awake()
    {
        playerTurnManager.PlayerTurnsChanged += OnPlayerTurnsChanged;
        playerTurnManager.TurnChanged += OnTurnChanged;
    }

    private void OnDestroy()
    {
        playerTurnManager.PlayerTurnsChanged -= OnPlayerTurnsChanged;
        playerTurnManager.TurnChanged -= OnTurnChanged;
    }

    private void OnTurnChanged(BattlePlayerTurn battlePlayerTurn)
    {
        if (_lastPlayerTurn != null)
        {
            _lastPlayerTurn.DisableGlow();
        }
        var playerTurn = _turnCollection
            .Where(p => p.BattlePlayerTurn == battlePlayerTurn)
            .Select(p => p.PlayerTurn)
            .FirstOrDefault();
        playerTurn?.EnableGlow();
        _lastPlayerTurn = playerTurn;
    }

    private void OnPlayerTurnsChanged(List<BattlePlayerTurn> list)
    {
        ClearPlayerTurnsPanel();
        SetupPlayerTurns(list);
    }

    private void ClearPlayerTurnsPanel()
    {
        foreach (Transform playerTurn in transform)
        {
            Destroy(playerTurn.gameObject);
        }
    }

    private void SetupPlayerTurns(List<BattlePlayerTurn> battlePlayerTurns)
    {
        var offsetY = 0f;
        foreach (var battlePlayerTurn in battlePlayerTurns)
        {
            var playerTurnTransform = Instantiate(playerTurnPrefab);
            playerTurnTransform.SetParent(transform);

            var playerTurnRectTransform = playerTurnTransform.GetComponent<RectTransform>();
            playerTurnRectTransform.anchoredPosition3D = new Vector3(0f, offsetY, 0f);

            offsetY -= playerTurnRectTransform.rect.height;

            var playerTurn = playerTurnTransform.GetComponent<PlayerTurn>();
            playerTurn.PlayerTurnImage.sprite = battlePlayerTurn.BattlePlayer.BattleUnit.ScriptableObject.icon;

            _turnCollection.Add((battlePlayerTurn, playerTurn));
        }
    }
}
