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
    public event Action<Vector3> MouseLeftClicked;
    public event Action<Vector2> MouseMoved;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Player.Interact.performed += InteractOnPerformed;
        playerInputActions.Player.MouseLeft.performed += MouseLeftPerformed;

        playerInputActions.Player.MouseMove.performed += MouseMovePerformed;
        playerInputActions.Player.MouseMove.canceled += MouseMoveCanceled;
    }

    private void MouseMoveCanceled(InputAction.CallbackContext obj)
    {
        var newPosition = Vector2.zero;
        MouseMoved?.Invoke(newPosition);
    }

    private void MouseMovePerformed(InputAction.CallbackContext context)
    {
        var newPosition = context.ReadValue<Vector2>();
        MouseMoved?.Invoke(newPosition);
    }

    private void MouseLeftPerformed(InputAction.CallbackContext context)
    {
        var mousePosition = Input.mousePosition;
        MouseLeftClicked?.Invoke(mousePosition);
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
