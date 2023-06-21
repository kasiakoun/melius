using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonCollider : MonoBehaviour
{
    [SerializeField] private Hexagon model;

    public Hexagon GetModel() => model;
}
