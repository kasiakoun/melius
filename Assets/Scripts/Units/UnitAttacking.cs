using System.Collections;
using System.Linq;
using UnityEngine;

public class UnitAttacking : MonoBehaviour
{
    [SerializeField] private WeaponBehavior[] weaponBehaviors;

    private UnitEquipment unitEquipment;

    private void Awake()
    {
        unitEquipment = GetComponent<UnitEquipment>();
    }

    public IEnumerator Attack(BattleUnitBase targetBattleUnit)
    {
        var weaponBehavior = GetWeaponBehavior();
        if (weaponBehavior == null) yield break;

        yield return weaponBehavior.Attack(unitEquipment?.Weapon, targetBattleUnit);
    }

    public bool IsMelee()
    {
        var weaponBehavior = GetWeaponBehavior();

        return weaponBehavior == null || weaponBehavior.IsMelee;
    }

    private WeaponBehavior GetWeaponBehavior()
    {
        var weapon = unitEquipment?.Weapon;
        var weaponBehavior = weaponBehaviors.FirstOrDefault(p => p.IsWeaponType(weapon));
        if (weaponBehavior == null)
        {
            Debug.LogError($"weaponBehavior was not found in collection for '{weapon}' weapon");
            return null;
        }

        return weaponBehavior;
    }
}
