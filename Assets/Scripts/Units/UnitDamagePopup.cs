using DG.Tweening;
using TMPro;
using UnityEngine;

public class UnitDamagePopup : MonoBehaviour
{
    private TextMeshPro textPopup;

    private bool isShown;

    private void Awake()
    {
        textPopup = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if (!isShown) return;

        var moveYSpeed = 0.5f;
        //var newPosition = new Vector3(transform.position.x, transform.position.y + moveYSpeed * Time.deltaTime, transform.position.z);
        //transform.position = newPosition;
    }

    public void Setup(int damage)
    {
        if (isShown) return;

        isShown = true;
        textPopup.text = damage.ToString();
    }
}
