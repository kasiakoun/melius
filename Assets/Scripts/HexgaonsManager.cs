using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexgaonsManager : MonoBehaviour
{
    [SerializeField] private LayerMask hexagonLayerMask;
    public static HexgaonsManager Instance { get; private set; }

    private Hexagon[] hexagons;
    public Hexagon SelectedHexagon { get; private set; }

    private HexgaonsManager() { }

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

    public Hexagon FindHexagonByRay(Ray ray)
    {
        var maxDistance = 30f;
        if (!Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, hexagonLayerMask))
        {
            return null;
        }

        var hexagonCollider = raycastHit.transform.GetComponent<HexagonCollider>();
        if (hexagonCollider == null)
        {
            return null;
        }

        return hexagonCollider.GetModel();
    }

    public Hexagon AnyHexagon() => hexagons[0];
}
