using System.Collections;
using UnityEngine;

public class FlyingRock : EnhancedObject
{
    [SerializeField] private ParticleSystem activateEffect;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(originalPosition.x, transform.position.y, originalPosition.z);
        transform.rotation = originalRotation;
    }

    public override void ActivateObject()
    {
        activateEffect.Play();
        activateEffect.transform.parent = null;
    }

    public override void DeactivateObject()
    {
        activateEffect.Stop();
    }
}
