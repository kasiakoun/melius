using System;
using UnityEngine;

public class PositionPicker : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private LayerMask terrainLayerMask;

    private bool isPicking;

    public event Action<Vector3> PositionPicked;

    public bool HandleLeftClick(Vector3 vector)
    {
        if (!isPicking) return false;

        var ray = Camera.main.ScreenPointToRay(vector);
        var contactPoint = GetContactPoint(ray);
        if (!contactPoint.HasValue) return false;

        Debug.Log($"contact point: {contactPoint.Value}");
        PositionPicked?.Invoke(contactPoint.Value);

        StopPicking();
        return true;
    }

    private Vector3? GetContactPoint(Ray ray)
    {
        var maxDistance = 1000f;
        if (!Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, terrainLayerMask))
        {
            return null;
        }

        return raycastHit.point;
    }

    public void StartPicking()
    {
        isPicking = true;
    }

    public void StopPicking()
    {
        isPicking = false;
    }
}
