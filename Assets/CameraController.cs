using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    private float sensitivity = 1.0f;
    private float minYAngle = -30.0f;
    private float maxYAngle = 45.0f;

    private float currentRotationX;
    private float currentRotationY;

    public void Start()
    {
        gameInput.MouseMoved += OnMouseMoved;
    }

    private void OnMouseMoved(Vector2 mouseDelta)
    {
        // Update the camera's vertical and horizontal rotation
        currentRotationX -= mouseDelta.y * sensitivity;
        currentRotationX = Mathf.Clamp(currentRotationX, minYAngle, maxYAngle);

        currentRotationY += mouseDelta.x * sensitivity;

        // Rotate the camera based on the delta
        var rotation = new Vector3(currentRotationX, currentRotationY, 0);
        transform.eulerAngles = rotation;
    }
}
