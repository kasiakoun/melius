using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(UnitEquipment))]
public class UnitPlayerAttacking : MonoBehaviour
{
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private WeaponBehavior[] weaponBehaviors;
    [SerializeField] private float delayBeforeHit;

    private UnitEquipment unitEquipment;

    private void Awake()
    {
        unitEquipment = GetComponent<UnitEquipment>();
    }

    public IEnumerator Attack(IBattleUnit targetBattleUnit)
    {
        var weapon = unitEquipment.Weapon;
        var weaponBehavior = weaponBehaviors.FirstOrDefault(p => p.IsWeaponType(weapon));
        if (weaponBehavior == null)
        {
            Debug.LogError($"weaponBehavior was not found in collection for '{weapon}' weapon");
            yield break;
        }
        yield return weaponBehavior.Attack(weapon, targetBattleUnit);
        // todo: place this code into separate behavior
        //playerAnimator.RightMeleeAttack();
        //yield return new WaitForSeconds(delayBeforeHit);
        //targetBattleUnit.TakeDamage();
    }
}
