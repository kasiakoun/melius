using UnityEngine;

public class HealthBarContainer : MonoBehaviour
{
    [SerializeField] private Transform healthBarHolder;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
        transform.position = healthBarHolder.position;
    }
}