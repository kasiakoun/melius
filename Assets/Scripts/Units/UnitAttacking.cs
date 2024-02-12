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
        var weapon = unitEquipment?.Weapon;
        var weaponBehavior = weaponBehaviors.FirstOrDefault(p => p.IsWeaponType(weapon));
        if (weaponBehavior == null)
        {
            Debug.LogError($"weaponBehavior was not found in collection for '{weapon}' weapon");
            yield break;
        }
        yield return weaponBehavior.Attack(weapon, targetBattleUnit);
    }
}
