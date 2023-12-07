using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(UnitPlayerAttacking))]
[RequireComponent(typeof(UnitPlayerDamageable))]
[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(UnitRotation))]
[RequireComponent(typeof(UnitHealth))]
[RequireComponent(typeof(UnitEquipment))]
public class PlayerBattleUnit : MonoBehaviour, IBattleUnit
{
    [SerializeField] private UnitScriptableObject scriptableObject;

    private NavMeshAgent navMeshAgent;
    private Outline outline;
    private UnitPlayerAttacking unitAttacking;
    private UnitPlayerDamageable unitDamageable;
    private UnitMovement unitMovement;
    private UnitRotation unitRotation;
    private UnitHealth unitHealth;
    private UnitEquipment unitEquipment;
    private WeaponScriptableObject weaponScriptableObject;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        outline = GetComponent<Outline>();
        unitAttacking = GetComponent<UnitPlayerAttacking>();
        unitDamageable = GetComponent<UnitPlayerDamageable>();
        unitMovement = GetComponent<UnitMovement>();
        unitRotation = GetComponent<UnitRotation>();
        unitHealth = GetComponent<UnitHealth>();
        unitHealth.SetMaxHealth(scriptableObject.initialMaxHealth);
        unitEquipment = GetComponent<UnitEquipment>();

        // todo: get weapon ScriptableObject from another place(inventory or something like this)
        weaponScriptableObject = Resources.Load<WeaponScriptableObject>("ScriptableObject/Weapons/Bow");
        unitEquipment.SetWeaponByPrefab(weaponScriptableObject.prefab);
    }

    public bool IsWalking() => unitMovement.IsWalking;

    #region IBattleUnit Implementation

    public Vector3 Position => transform.position;
    public UnitScriptableObject ScriptableObject => scriptableObject;

    public IEnumerator Move(Vector3 destination) => unitMovement.Move(destination);

    public IEnumerator Rotate(Vector3 position) => unitRotation.Rotate(position);

    public void TakeDamage() => unitDamageable.TakeDamage();

    public IEnumerator Attack(IBattleUnit targetBattleUnit) => unitAttacking.Attack(targetBattleUnit);

    public void SetHighlightOutline(bool enable) => outline.enabled = enable;

    #endregion
}
