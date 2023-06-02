using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    public event Action PlayerInteracted;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Player.Interact.performed += InteractOnPerformed;
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
