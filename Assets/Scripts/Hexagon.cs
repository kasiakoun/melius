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

    public Renderer GetHexagonRenderer() => hexagonRenderer;
}