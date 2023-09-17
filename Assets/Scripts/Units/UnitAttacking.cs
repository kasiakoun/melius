using System.Collections;
using UnityEngine;

public class UnitAttacking : MonoBehaviour
{
    [SerializeField] private UnitAnimator unitAnimator;
    [SerializeField] private float delayBeforeHit;

    public IEnumerator Attack()
    {
        unitAnimator.Attack();
        yield return new WaitForSeconds(delayBeforeHit);
    }
}
