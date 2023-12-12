using System;
using System.Collections;
using UnityEngine;

public class SimpleBehavior : WeaponBehavior
{
    [SerializeField] private BaseUnitAnimator unitAnimator;
    [SerializeField] private float delayBeforeHit;

    public override IEnumerator Attack(Weapon weapon, IBattleUnit targetBattleUnit)
    {
        unitAnimator.SimpleAttack();
        yield return new WaitForSeconds(delayBeforeHit);
    }
}
