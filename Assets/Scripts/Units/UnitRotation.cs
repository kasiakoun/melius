using System.Collections;
using UnityEngine;

public class UnitRotation : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10.0f;

    private readonly float rotationThreshold = 5.0f;

    public IEnumerator Rotate(Vector3 position)
    {
        position.y = 0.0f;
        var transformPosition = transform.position;
        transformPosition.y = 0.0f;

        var targetRotation = Quaternion.LookRotation(position - transformPosition);

        while (Mathf.Abs(Quaternion.Angle(targetRotation, transform.rotation)) > rotationThreshold)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
