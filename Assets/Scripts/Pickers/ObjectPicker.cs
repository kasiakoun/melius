using System;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    [SerializeField] private LayerMask objectLayerMask;

    public event Action<EnhancedObject> ObjectPicked;

    public bool HandleLeftClick(Vector3 vector)
    {
        var ray = Camera.main.ScreenPointToRay(vector);
        var enhancedObject = GetEnhancedObjectByRay(ray);
        if (enhancedObject == null) return false;

        ObjectPicked?.Invoke(enhancedObject);

        return true;
    }

    private EnhancedObject GetEnhancedObjectByRay(Ray ray)
    {
        var maxDistance = 1000f;
        if (!Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, objectLayerMask))
        {
            return null;
        }

        return raycastHit.transform.GetComponent<EnhancedObject>();
    }
}
