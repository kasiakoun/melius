using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;

public class Player : MonoBehaviour, ITestObjectParent
{
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform holder;
    [SerializeField] private LayerMask hexagonLayerMask;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private TestObject testObject;

    public event Action<BaseCounter> SelectedCounterChanged;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameInput.PlayerInteracted += OnPlayerInteracted;
        gameInput.PlayerClicked += OnPlayerClicked;
    }

    private void OnPlayerClicked(Vector3 vector)
    {
        var ray = Camera.main.ScreenPointToRay(vector);
        Debug.Log($"ray: {ray}");
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 100, false);

        var maxDistance = 100f;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, hexagonLayerMask))
        {
            var hexagonCollider = raycastHit.transform.GetComponent<HexagonCollider>();
            if (hexagonCollider != null)
            {
                var hexagon = hexagonCollider.GetModel();
                HexgaonsManager.Instance.SelectHexagon(hexagon);
            }
        }
    }

    private void OnPlayerInteracted()
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        var interactDistance = 1f;
        Debug.DrawRay(transform.position, lastInteractDir * 100, Color.green);
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            Debug.Log("Raycasted");
            if (raycastHit.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                if (selectedCounter != clearCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter clearCounter)
    {
        selectedCounter = clearCounter;
        //Debug.Log(clearCounter);
        SelectedCounterChanged?.Invoke(clearCounter);
    }

    private void HandleMovement()
    {
        var normalizedVector = gameInput.GetMovementVectorNormalized();
        isWalking = normalizedVector != Vector2.zero;

        var moveDirection = new Vector3(normalizedVector.x, 0, normalizedVector.y);

        var maxDistance = Time.deltaTime * moveSpeed;
        var playerRadius = 0.5f;
        var playerHeight = 2.0f;
        var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, maxDistance);
        if (!canMove)
        {
            var moveDirectionByX = new Vector3(moveDirection.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionByX, maxDistance);
            if (canMove)
            {
                moveDirection = moveDirectionByX.normalized;
            }
            else
            {
                var moveDirectionByZ = new Vector3(0, 0, moveDirection.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionByZ, maxDistance);
                if (canMove)
                {
                    moveDirection = moveDirectionByZ.normalized;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * maxDistance;
        }

        var rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public void SetTestObject(TestObject testObject)
    {
        this.testObject = testObject;
    }

    public TestObject GetTestObject()
    {
        return testObject;
    }

    public void ClearTestObject()
    {
        testObject = null;
    }

    public bool HasTestObject()
    {
        return testObject != null;
    }

    public Transform GetHolderTransform()
    {
        return holder;
    }
}
