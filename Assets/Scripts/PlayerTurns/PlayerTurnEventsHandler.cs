using System;
using UnityEngine;

public class PlayerTurnEventsHandler : MonoBehaviour
{
    public event Action<IBattleTurnPlayer> PlayerTurnIsCame;
    public event Action<IBattleTurnPlayer> PlayerStartActing;
    public event Action<IBattleTurnPlayer> PlayerFinishActing;

    public void OnPlayerTurnIsCame(IBattleTurnPlayer player) => PlayerTurnIsCame?.Invoke(player);
    public void OnPlayerStartActing(IBattleTurnPlayer player) => PlayerStartActing?.Invoke(player);
    public void OnPlayerFinishActing(IBattleTurnPlayer player) => PlayerFinishActing?.Invoke(player);
}
