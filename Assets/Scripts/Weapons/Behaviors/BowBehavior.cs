using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowBehavior : WeaponBehavior<Bow>
{
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private Transform arrowPrefab;
    [SerializeField] private Transform arrowHolder;
    [SerializeField] private float arrowSpeed = 1.0f;
    [SerializeField] private float arrowRotateSpeed = 100.0f;
    [SerializeField] private float waitSecondsToShot = 0.45f;

    public override IEnumerator Attack(Weapon weapon, IBattleUnit targetBattleUnit)
    {
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

        var targetPosition = targetBattleUnit.Position;
        var maxDistance = 0.05f;
        var moveDirection = (targetPosition - arrow.position).normalized;
        while (Vector3.Distance(arrow.position, targetPosition) > maxDistance)
        {
            // todo: arrow rotation does not work
            arrow.position = Vector3.MoveTowards(arrow.position, targetPosition, Time.fixedDeltaTime * arrowSpeed);
            arrow.forward = Vector3.Slerp(arrow.forward, moveDirection, Time.fixedDeltaTime * arrowRotateSpeed);
            Debug.Log($"arrow.position = {arrow.position}");
            yield return new WaitForFixedUpdate();
        }

        Destroy(arrow.gameObject);
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
