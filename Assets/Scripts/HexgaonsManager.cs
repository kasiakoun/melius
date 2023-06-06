using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexgaonsManager : MonoBehaviour
{
    public static HexgaonsManager Instance { get; private set; }

    private Hexagon[] hexagons;
    private Hexagon selectedHexagon;

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
        selectedHexagon?.Unselect();
        selectedHexagon = hexagon;
        selectedHexagon.Select();
    }
}
