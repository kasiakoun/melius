using System.Collections;
using System.Drawing;
using UnityEngine;

public class BowBehavior : WeaponBehavior<Bow>
{
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private Transform arrowPrefab;
    [SerializeField] private Transform arrowHolder;
    [SerializeField] private float arrowSpeed = 7f;
    [SerializeField] private float arrowRotationSpeed = 3f;
    [SerializeField] private float waitSecondsToShot = 0.45f;

    public override bool IsMelee => false;

    public override IEnumerator Attack(Weapon weapon, BattleUnitBase targetBattleUnit)
    {
        //yield return new WaitForSeconds(2);
        var bow = weapon as Bow;
        if (bow == null)
        {
            Debug.LogError("weapon parameter is not Bow type or weapon is null");
            yield break;
        }

        var arrow = CreateArrow();
        var bowAnimator = bow.BowAnimator;
        bowAnimator.ActivateBow();
        playerAnimator.BowShot();
        yield return new WaitForSeconds(waitSecondsToShot);
        arrow.parent = null;

        var targetPosition = targetBattleUnit.TargetPosition;
        var maxDistance = 0.75f;
        while (Vector3.Distance(arrow.position, targetPosition) > maxDistance)
        {
            arrow.position = Vector3.MoveTowards(arrow.position, targetPosition, Time.fixedDeltaTime * arrowSpeed);

            var relativePos = targetPosition - arrow.position;
            var toRotation = Quaternion.LookRotation(relativePos);
            arrow.rotation = Quaternion.Lerp(arrow.rotation, toRotation, Time.deltaTime * arrowRotationSpeed);

            Debug.Log($"arrow.position = {arrow.position}");
            yield return new WaitForFixedUpdate();
        }

        // todo: replace this line with another behavior
        //Destroy(arrow.gameObject);
    }

    private Transform CreateArrow()
    {
        var arrow = Instantiate(arrowPrefab);

        arrow.parent = arrowHolder;
        arrow.localPosition = arrowPrefab.position;
        arrow.localEulerAngles = arrowPrefab.eulerAngles;
        arrow.localScale = arrowPrefab.localScale;

        return arrow;
    }
}
