using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarAttachingController : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
        //transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }
}