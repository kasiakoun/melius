using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class TestObjectSO : ScriptableObject
{
    public Transform prefab;
    public Mesh mesh;
    public string objectName;
}
