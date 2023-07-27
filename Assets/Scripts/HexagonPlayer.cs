using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private CameraManager cameraManager;

    private HexagonManager hexagonsManager;
    private Hexagon currentHexagon;
    private List<Hexagon> hexagonsPath;

    private bool isWalking;

    public static HexagonPlayer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hexagonsManager = HexagonManager.Instance;
        gameInput.PlayerClicked += OnPlayerClicked;
        InitializeCurrentHexagon();
    }

    public bool IsWalking() => isWalking;

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

    private void StartWalkingByHexagons(List<Hexagon> hexagons)
    {
        if (isWalking) return;

        isWalking = true;
        StartCoroutine(StartWalking(hexagons));
    }

    private IEnumerator StartWalking(List<Hexagon> hexagons)
    {
        cameraManager.SwitchToPlayerCamera();
        foreach (var hexagon in hexagons)
        {
            yield return HandleMovementToHexagon(hexagon);
            currentHexagon = hexagon;
        }

        cameraManager.SwitchToWorldCamera();

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

        transform.position = hegaonCenter;
    }
}
