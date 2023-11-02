using UnityEngine;
using UnityEngine.UI;

public class PlayerTurn : MonoBehaviour
{
    [SerializeField] private Image playerTurnImage;
    [SerializeField] private GameObject turnGlow;

    public Image PlayerTurnImage => playerTurnImage;

    public void EnableGlow() => turnGlow.SetActive(true);
    public void DisableGlow() => turnGlow.SetActive(false);
}
