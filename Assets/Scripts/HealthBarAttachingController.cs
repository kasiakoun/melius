using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarAttachingController : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] private Renderer hpBarRenderer;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
        //rend
        //transform.RotateAround(hpBarRenderer.bounds.center, transform.up, angle);
        //transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        //transform.LookAt(mainCamera.transform);
    }
}