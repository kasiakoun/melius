using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnsPanel : MonoBehaviour
{
    [SerializeField] private PlayerTurnManager playerTurnManager;
    [SerializeField] private GameObject playerTurns;
    [SerializeField] private Transform playerTurnPrefab;

    private void Awake()
    {
        playerTurnManager.PlayerTurnsChanged += OnPlayerTurnsChanged;
    }

    private void OnDestroy()
    {
        playerTurnManager.PlayerTurnsChanged -= OnPlayerTurnsChanged;
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
        }
    }
}
