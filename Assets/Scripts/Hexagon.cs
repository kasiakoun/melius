using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    private const int SIDES_COUNT = 6;
    private const int ONE_SIDE_IN_DEGREES = 60;

    private float selectedSize = 10f;
    private float deltaY = 0.01f;
    public Transform hexgaonModel;

    [SerializeField] private Renderer hexagonRenderer;
    [SerializeField] private Transform selectedHexagonModel;
    [SerializeField] private LayerMask obstacleLayer;

    public Vector3 SideBoxSize { get; private set; } = new Vector3(2, 1, 1);
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get; private set; }

    public Hexagon CameFromHexagon { get; set; }

    public void OnDrawGizmosSelected()
    {
        var hexagonRadius = hexagonRenderer.bounds.size.x / 2;
        var hexagonCenter = hexagonRenderer.bounds.center;
        for (var i = 0; i < SIDES_COUNT; i++)
        {
            var angle = i * ONE_SIDE_IN_DEGREES;

            var angleVector = new Vector3(0, -angle, 0);
            var rotation = Quaternion.Euler(angleVector);

            var newX = hexagonCenter.x + hexagonRadius * Mathf.Cos((angle * Mathf.PI) / 180);
            var newZ = hexagonCenter.z + hexagonRadius * Mathf.Sin((angle * Mathf.PI) / 180);
            var nearbyVector = new Vector3(newX, 0, newZ);
            var position = nearbyVector;

            var colliders = Physics.OverlapBox(position, SideBoxSize / 2, rotation, obstacleLayer);
            Gizmos.color = colliders != null && colliders.Length > 0
                ? Color.red
                : Color.green;

            Gizmos.matrix = Matrix4x4.TRS(position, rotation, SideBoxSize);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }

    public void Select()
    {
        hexgaonModel.transform.localScale = new Vector3(
            hexgaonModel.transform.localScale.x - selectedSize,
            hexgaonModel.transform.localScale.y - selectedSize,
            hexgaonModel.transform.localScale.z - selectedSize);

        selectedHexagonModel.gameObject.SetActive(true);

        hexgaonModel.transform.localPosition = new Vector3(
            hexgaonModel.transform.localPosition.x,
            hexgaonModel.transform.localPosition.x + deltaY,
            hexgaonModel.transform.localPosition.z);
    }

    public void Unselect()
    {
        hexgaonModel.transform.localScale = new Vector3(
            hexgaonModel.transform.localScale.x + selectedSize,
            hexgaonModel.transform.localScale.y + selectedSize,
            hexgaonModel.transform.localScale.z + selectedSize);

        selectedHexagonModel.gameObject.SetActive(false);

        hexgaonModel.transform.localPosition = new Vector3(
            hexgaonModel.transform.localPosition.x,
            hexgaonModel.transform.localPosition.y - deltaY,
            hexgaonModel.transform.localPosition.z);
    }

    public void CalculateFCost()
    {
        FCost = GCost + HCost;
    }

    public Renderer GetHexagonRenderer() => hexagonRenderer;
}