using System.Collections;
using UnityEngine;

public class UnitPlayerAttacking : MonoBehaviour
{
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private float delayBeforeHit;

    public IEnumerator Attack()
    {
        playerAnimator.RightMeleeAttack();
        yield return new WaitForSeconds(delayBeforeHit);
    }
}
