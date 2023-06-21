using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonPathRenderer : MonoBehaviour
{
    private const int IMAGES_BETWEEN_NEARBY_HEXAGONS = 4;

    [SerializeField] private Transform stepImagePrefab;
    [SerializeField] private Transform finishSteoImagePrefab;
    [SerializeField] private Transform hexagonPathContainer;

    public void Start()
    {
        ClearCurrentPath();
    }

    public void RenderPath(List<Hexagon> hexagonsPath)
    {
        ClearCurrentPath();

        for (var i = 1; i < hexagonsPath.Count; i++)
        {
            var startHexagon = hexagonsPath[i - 1];
            var endHexagon = hexagonsPath[i];

            var isLastStep = i + 1 >= hexagonsPath.Count;
            RenderPathBetweenNearbyHexagons(startHexagon, endHexagon, isLastStep);
        }
    }
    
    private void ClearCurrentPath()
    {
        foreach (Transform child in hexagonPathContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void RenderPathBetweenNearbyHexagons(Hexagon startHexagon, Hexagon endHexagon, bool isLastHexagon)
    {
        var startHexagonRenderer = startHexagon.GetHexagonRenderer();
        var startHexagonCenter = startHexagonRenderer.bounds.center;

        var endHexagonRenderer = endHexagon.GetHexagonRenderer();
        var endHexagonCenter = endHexagonRenderer.bounds.center;

        for (var i = 1; i <= IMAGES_BETWEEN_NEARBY_HEXAGONS; i++)
        {
            var vector = (endHexagonCenter - startHexagonCenter) / (IMAGES_BETWEEN_NEARBY_HEXAGONS / (float)i);
            var pointOnVector = vector + startHexagonCenter;

            var isLastImage = i + 1 > IMAGES_BETWEEN_NEARBY_HEXAGONS;
            if (isLastHexagon && isLastImage)
            {
                RenderFinishStep(pointOnVector);
            }
            else
            {
                RenderStep(pointOnVector);
            }
        }
    }

    private void RenderStep(Vector3 pointOnVector) => RenderImage(stepImagePrefab, pointOnVector);
    private void RenderFinishStep(Vector3 pointOnVector) => RenderImage(finishSteoImagePrefab, pointOnVector);

    private void RenderImage(Transform prefab, Vector3 pointOnVector)
    {
        var pathCircle = Instantiate(prefab);
        pathCircle.parent = hexagonPathContainer;
        pathCircle.position = new Vector3(pointOnVector.x, prefab.position.y, pointOnVector.z);
    }
}
