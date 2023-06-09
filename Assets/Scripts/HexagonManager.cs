using System.Collections;
using System.Collections.Generic;
using Stopwatch = System.Diagnostics.Stopwatch;
using System.Linq;
using UnityEngine;

public class HexagonManager : MonoBehaviour
{
    [SerializeField] private LayerMask hexagonLayerMask;
    [SerializeField] private HexagonPathFinder hexagonPathFinder;
    [SerializeField] private HexagonPathRenderer hexagonPathRenderer;
    public static HexagonManager Instance { get; private set; }

    public Hexagon[] hexagons;
    public Hexagon SelectedHexagon { get; private set; }

    private HexagonManager() { }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hexagons = FindObjectsOfType<Hexagon>();
    }

    public void SelectHexagon(Hexagon hexagon)
    {
        SelectedHexagon?.Unselect();
        SelectedHexagon = hexagon;
        SelectedHexagon.Select();
    }

    public void RenderHexagonPath(List<Hexagon> hexagonsPath)
    {
        hexagonPathRenderer.RenderPath(hexagonsPath);
    }

    public List<Hexagon> FindPath(Hexagon startHexagon, Hexagon endHexagon)
    {
        return hexagonPathFinder.FindPath(startHexagon, endHexagon);
    }
    
    public List<Hexagon> GetDirectHexagonsPath(Hexagon startHexagon, Hexagon endHexagon)
    {
        if (startHexagon == endHexagon) return null;

        var endHexagonRenderer = endHexagon.GetHexagonRenderer();
        var endHexagonCenter = endHexagonRenderer.bounds.center;

        var closestHexagon = startHexagon;
        var hexagonsPath = new List<Hexagon>();
        do
        {
            var nearbyHexagons = FindNearbyHexagons(closestHexagon);
            closestHexagon = nearbyHexagons
                .OrderBy(p => Vector3.Distance(p.GetHexagonRenderer().bounds.center, endHexagonCenter))
                .FirstOrDefault();
            hexagonsPath.Add(closestHexagon);
        }
        while (closestHexagon != endHexagon);

        return hexagonsPath;
    }

    public Hexagon FindHexagonByRay(Ray ray)
    {
        var maxDistance = 100f;
        if (!Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, hexagonLayerMask))
        {
            return null;
        }

        return GetHexagonFromTransform(raycastHit.transform);
    }

    public Hexagon[] FindNearbyHexagons(Hexagon hexagon)
    {
        var renderer = hexagon.GetHexagonRenderer();

        var hexagonSize = renderer.bounds.size;
        var hexagonCenter = renderer.bounds.center;

        var radius = hexagonSize.x;
        var colliders = Physics.OverlapSphere(hexagonCenter, radius, hexagonLayerMask);
        var nearbyHexagons = colliders
            .Select(p => GetHexagonFromTransform(p.transform))
            .Where(p => p != hexagon)
            .ToArray();

        return nearbyHexagons;
    }

    private Hexagon GetHexagonFromTransform(Transform hexgaonTransform)
    {
        var hexagonCollider = hexgaonTransform.GetComponent<HexagonCollider>();
        if (hexagonCollider == null)
        {
            return null;
        }

        return hexagonCollider.GetModel();
    }

    public Hexagon AnyHexagon() => hexagons[0];
}
