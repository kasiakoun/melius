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

    private HexgaonsManager hexagonsManager;
    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private TestObject testObject;
    private Hexagon currentHexagon;
    private List<Hexagon> hexagonsPath;

    public event Action<BaseCounter> SelectedCounterChanged;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hexagonsManager = HexgaonsManager.Instance;
        gameInput.PlayerInteracted += OnPlayerInteracted;
        gameInput.PlayerClicked += OnPlayerClicked;
        InitializeCurrentHexagon();
    }

    private void InitializeCurrentHexagon()
    {
        currentHexagon = GetPlayerHexagon();
        if (currentHexagon == null)
        {
            currentHexagon = hexagonsManager.AnyHexagon();
        }

        InitializePlayerPosition(currentHexagon);
    }

    private Hexagon GetPlayerHexagon()
    {
        var position = transform.position;
        var direction = Vector3.down;
        var ray = new Ray(position, direction);

        return hexagonsManager.FindHexagonByRay(ray);
    }

    private void InitializePlayerPosition(Hexagon hexagon)
    {
        var hexagonRenderer = hexagon.GetHexagonRenderer();
        var hexagonCenter = hexagonRenderer.bounds.center;

        var moveDirection = (hexagonCenter - transform.position).normalized;

        transform.forward = moveDirection;
        transform.position = hexagonCenter;
    }

    private void OnPlayerClicked(Vector3 vector)
    {
        if (isWalking) return;
        var ray = Camera.main.ScreenPointToRay(vector);
        //Debug.Log($"ray: {ray}");
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 100, false);

        var selectedHexagon = hexagonsManager.FindHexagonByRay(ray);
        if (selectedHexagon != null)
        {
            if (selectedHexagon == hexagonsManager.SelectedHexagon)
            {
                StartWalkingByHexagons(hexagonsPath);
            }
            else
            {
                hexagonsManager.SelectHexagon(selectedHexagon);
                hexagonsPath = hexagonsManager.FindPath(currentHexagon, selectedHexagon);
                hexagonsManager.RenderHexagonPath(hexagonsPath);
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
        //HandleMovement();
        //HandleInteractions();
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

    private void StartWalkingByHexagons(List<Hexagon> hexagons)
    {
        if (isWalking) return;

        isWalking = true;
        StartCoroutine(StartWalking(hexagons));
    }

    private IEnumerator StartWalking(List<Hexagon> hexagons)
    {
        foreach (var hexagon in hexagons)
        {
            yield return HandleMovementToHexagon(hexagon);
            currentHexagon = hexagon;
        }

        isWalking = false;
    }

    private IEnumerator HandleMovementToHexagon(Hexagon hexagon)
    {
        var hexagonRenderer = hexagon.GetHexagonRenderer();
        var hegaonCenter = hexagonRenderer.bounds.center;

        var rotateSpeed = 10f;
        var moveDirection = (hegaonCenter - transform.position).normalized;

        var maxDistance = 0.05f;
        while (Vector3.Distance(transform.position, hegaonCenter) > maxDistance)
        {
            var maxDistanceDelta = Time.fixedDeltaTime * moveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, hegaonCenter, maxDistanceDelta);

            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.fixedDeltaTime * rotateSpeed);
            yield return new WaitForFixedUpdate();
        }
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
