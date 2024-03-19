using TMPro;
using UnityEngine;

public class UnitDamageText : MonoBehaviour
{
    [SerializeField] private UnitDamagePopup damagePopupPrefab;
    [SerializeField] private Transform damagePopupHolder;

    public void Show(int damage)
    {
        var damagePopup = Instantiate<UnitDamagePopup>(damagePopupPrefab, damagePopupHolder.transform.position, Quaternion.identity);
        damagePopup.Setup(damage);
    }
}
