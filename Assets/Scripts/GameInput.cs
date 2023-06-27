using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private int mouseClickedTimes;

    public event Action PlayerInteracted;
    public event Action<Vector3> PlayerClicked;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Player.Interact.performed += InteractOnPerformed;
        playerInputActions.Player.MouseMove.performed += MouseMoveClicked;
    }

    private void MouseMoveClicked(InputAction.CallbackContext context)
    {
        var mousePosition = Input.mousePosition;
        // Debug.Log($"Mouse clicked times: {mouseClickedTimes++} mousePosition = {mousePosition}");
        PlayerClicked?.Invoke(mousePosition);
    }

    private void InteractOnPerformed(InputAction.CallbackContext obj)
    {
        PlayerInteracted?.Invoke();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        var inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }
}
