using System.Collections;
using UnityEngine;

public class UnitRotation : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10.0f;

    private readonly float rotationThreshold = 5.0f;

    public IEnumerator Rotate(Vector3 position)
    {
        var targetRotation = Quaternion.LookRotation(position - transform.position);

        while (Mathf.Abs(Quaternion.Angle(targetRotation, transform.rotation)) > rotationThreshold)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
