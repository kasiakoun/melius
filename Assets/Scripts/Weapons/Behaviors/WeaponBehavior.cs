using System;
using System.Collections;
using UnityEngine;

public abstract class WeaponBehavior<TWeapon> : WeaponBehavior where TWeapon : Weapon
{
    public override Type WeaponType => typeof(TWeapon);
}

public abstract class WeaponBehavior : MonoBehaviour
{
    public virtual Type WeaponType { get; }
    public abstract bool IsMelee { get; }
    public abstract IEnumerator Attack(Weapon weapon, BattleUnitBase targetBattleUnit);

    public bool IsWeaponType(Weapon weapon)
    {
        if (weapon == null && WeaponType == null) return true;

        return weapon != null && weapon.GetType() == WeaponType;
    }
}