using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    private float selectedSize = 10f;
    private float deltaY = 0.01f;
    public Transform hexgaonModel;
    [SerializeField] private Renderer hexagonRenderer;
    [SerializeField] private Transform selectedHexagonModel;

    [SerializeField] private Transform rightTopSide;
    [SerializeField] private Transform rightSide;
    [SerializeField] private Transform rightBottomSide;
    [SerializeField] private Transform leftBottomSide;
    [SerializeField] private Transform leftSide;
    [SerializeField] private Transform leftTopSide;
    [SerializeField] private LayerMask obstacleLayer;

    private List<Transform> sides => new List<Transform> { rightTopSide, rightSide, rightBottomSide, leftBottomSide, leftSide, leftTopSide };


    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get; private set; }

    public Hexagon CameFromHexagon { get; set; }

    public void OnDrawGizmosSelected()
    {
        //var renderer = GetHexagonRenderer();

        //var hexagonSize = renderer.bounds.size;
        //var hexagonCenter = renderer.bounds.center;

        //var radius = hexagonSize.x * 10;

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(hexagonCenter, radius);

        foreach (var side in sides)
        {
            var position = side.position;
            var size = side.lossyScale;
            var rotation = side.rotation;

            var colliders = Physics.OverlapBox(position, size / 2, rotation, obstacleLayer);
            if (colliders != null && colliders.Length > 0)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }

            Gizmos.matrix = Matrix4x4.TRS(position, rotation, size);
            Gizmos.DrawWireCube(Vector3.zero, size);
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