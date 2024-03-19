using DG.Tweening;
using TMPro;
using UnityEngine;

public class UnitDamagePopup : MonoBehaviour
{
    private Camera mainCamera;
    private TextMeshPro textPopup;

    private bool isShown;

    private void Awake()
    {
        mainCamera = Camera.main;
        textPopup = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
        if (!isShown) return;

        var moveYSpeed = 0.5f;
        var hidingSpeed = 1.5f;
        var newPosition = new Vector3(
            transform.position.x,
            transform.position.y + moveYSpeed * Time.deltaTime,
            transform.position.z);
        transform.position = newPosition;

        var currentColor = textPopup.color;
        var newColorAlpha = currentColor.a - hidingSpeed * Time.deltaTime;
        currentColor.a = newColorAlpha;
        textPopup.color = currentColor;
        if (currentColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(int damage)
    {
        if (isShown) return;

        isShown = true;
        textPopup.text = damage.ToString();
    }
}
