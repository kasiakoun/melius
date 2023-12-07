using UnityEngine;

public class UnitEquipment : MonoBehaviour
{
    [SerializeField] private Transform leftHandHolder;
    [SerializeField] private Transform rightHandHolder;

    public Weapon Weapon { get; private set; }

    public void SetWeaponByPrefab(Transform weaponPrefab)
    {
        if (Weapon != null)
        {
            Weapon.transform.parent = null;
        }
        var weapon = Instantiate(weaponPrefab);
        // todo: there is need to choose specific holder depend on Weapon type
        weapon.parent = leftHandHolder;
        weapon.localPosition = weaponPrefab.position;
        weapon.localEulerAngles = weaponPrefab.eulerAngles;
        weapon.localScale = weaponPrefab.localScale;

        Weapon = weapon.GetComponent<Weapon>();
    }
}
