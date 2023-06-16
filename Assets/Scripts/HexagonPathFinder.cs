using System.Collections;
using System.Collections.Generic;
using Stopwatch = System.Diagnostics.Stopwatch;
using System.Linq;
using UnityEngine;

public class HexagonPathFinder : MonoBehaviour
{
    private HexgaonsManager hexagonManager;

    private List<Hexagon> openHexagons;
    private List<Hexagon> closedHexagons;

    public void Start()
    {
        hexagonManager = HexgaonsManager.Instance;
    }

    public List<Hexagon> FindPath(Hexagon startHexagon, Hexagon endHexagon)
    {
        if (startHexagon == null || endHexagon == null) return null;

        var stopwatch = Stopwatch.StartNew();

        openHexagons = new List<Hexagon> { startHexagon };
        closedHexagons = new List<Hexagon>();

        SetupHexagons();

        startHexagon.GCost = 0;
        startHexagon.HCost = CalculateDistanceCost(startHexagon, endHexagon);
        startHexagon.CalculateFCost();

        while (openHexagons.Count > 0)
        {
            var currentHexagon = GetLowestFCostHexagon(openHexagons);
            if (currentHexagon == endHexagon)
            {
                stopwatch.Stop();
                Debug.Log($"stopwatch to find hexgons path: {stopwatch.ElapsedMilliseconds}");
                return CalculatePath(endHexagon);
            }

            openHexagons.Remove(currentHexagon);
            closedHexagons.Add(currentHexagon);

            var heighbourHexagons = hexagonManager.FindNearbyHexagons(currentHexagon);
            foreach (var neighbourHexagon in heighbourHexagons)
            {
                if (closedHexagons.Contains(neighbourHexagon)) continue;
                // todo: check if can be walk otherwise add
                //if (!neighbourHexagon.isWalkable)
                //{
                //    closedHexagons.Add(neighbourHexagon);
                //    continue;
                //}

                var tentativeGCost = currentHexagon.GCost + CalculateDistanceCost(currentHexagon, neighbourHexagon);
                if (tentativeGCost >= neighbourHexagon.GCost) continue;

                neighbourHexagon.CameFromHexagon = currentHexagon;
                neighbourHexagon.GCost = tentativeGCost;
                neighbourHexagon.HCost = CalculateDistanceCost(neighbourHexagon, endHexagon);
                neighbourHexagon.CalculateFCost();

                if (!openHexagons.Contains (neighbourHexagon))
                {
                    openHexagons.Add(neighbourHexagon);
                }
            }
        }

        return null;
    }

    private int CalculateDistanceCost(Hexagon startHexagon, Hexagon endHexagon)
    {
        // todo: redo this alghoritm to much faster
        var directPath = hexagonManager.GetDirectHexagonsPath(startHexagon, endHexagon);
        if (directPath == null) return -1;

        return directPath.Count;
    }

    private List<Hexagon> CalculatePath(Hexagon endHexagon)
    {
        var path = new List<Hexagon>
        {
            endHexagon
        };

        var currentHexagon = endHexagon;
        while (currentHexagon.CameFromHexagon != null)
        {
            path.Add(currentHexagon.CameFromHexagon);
            currentHexagon = currentHexagon.CameFromHexagon;
        }

        path.Reverse();
        return path;
    }

    private void SetupHexagons()
    {
        // todo: get available hexaxgons;
        foreach (var hexagon in hexagonManager.hexagons)
        {
            hexagon.GCost = int.MaxValue;
            hexagon.CalculateFCost();
            hexagon.CameFromHexagon = null;
        }
    }

    private Hexagon GetLowestFCostHexagon(List<Hexagon> hexagons)
    {
        return hexagons.OrderBy(p => p.FCost).FirstOrDefault();
    }
}
