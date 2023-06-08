using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexgaonsManager : MonoBehaviour
{
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
}
