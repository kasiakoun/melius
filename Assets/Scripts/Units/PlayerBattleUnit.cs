using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(UnitAttacking))]
[RequireComponent(typeof(UnitDamageable))]
[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(UnitRotation))]
[RequireComponent(typeof(UnitHealth))]
[RequireComponent(typeof(UnitEquipment))]
[RequireComponent(typeof(UnitFlying))]
[RequireComponent(typeof(UnitEffects))]
public class PlayerBattleUnit : BattleUnitBase, IFlyingUnit
{
    [SerializeField] private UnitScriptableObject scriptableObject;

    private NavMeshAgent navMeshAgent;
    private Outline outline;
    private UnitAttacking unitAttacking;
    private UnitDamageable unitDamageable;
    private UnitMovement unitMovement;
    private UnitRotation unitRotation;
    private UnitHealth unitHealth;
    private UnitEquipment unitEquipment;
    private UnitFlying unitFlying;
    private UnitEffects unitEffects;
    private WeaponScriptableObject weaponScriptableObject;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        outline = GetComponent<Outline>();
        unitAttacking = GetComponent<UnitAttacking>();
        unitDamageable = GetComponent<UnitDamageable>();
        unitMovement = GetComponent<UnitMovement>();
        unitRotation = GetComponent<UnitRotation>();
        unitHealth = GetComponent<UnitHealth>();
        unitHealth.SetMaxHealth(scriptableObject.initialMaxHealth);
        unitEquipment = GetComponent<UnitEquipment>();
        unitFlying = GetComponent<UnitFlying>();
        unitEffects = GetComponent<UnitEffects>();

        // todo: get weapon ScriptableObject from another place(inventory or something like this)
        weaponScriptableObject = Resources.Load<WeaponScriptableObject>("ScriptableObject/Weapons/Bow");
        unitEquipment.SetWeaponByPrefab(weaponScriptableObject.prefab);
    }

    public bool IsWalking() => unitMovement.IsWalking;

    #region IBattleUnit Implementation

    public override Vector3 Position => transform.position;
    public override Vector3 TargetPosition => unitDamageable.Target.position;
    public override UnitScriptableObject ScriptableObject => scriptableObject;
    public override float StopDistanceToAttack => unitMovement.StopDistance;
    public override IEnumerator Move(Vector3 destination, float stopDistance = 1.0f) =>
        unitMovement.Move(destination, stopDistance);

    public override IEnumerator Rotate(Vector3 position) => unitRotation.Rotate(position);

    public override void TakeDamage() => unitDamageable.TakeDamage();

    public override IEnumerator Attack(BattleUnitBase targetBattleUnit) => unitAttacking.Attack(targetBattleUnit);
    public override bool IsMelee() => unitAttacking.IsMelee();

    public override void SetHighlightOutline(bool enable) => outline.enabled = enable;

    #endregion

    #region IFlyingUnit Implementation

    public Transform BaseHolder => unitFlying.BaseHolder;
    public bool IsFlying => unitFlying.IsFlying;
    public IEnumerator ActivateFlying() => unitFlying.ActivateObject();
    public IEnumerator DeactivateFlying() => unitFlying.DeactivateObject();

    #endregion

    #region IEffectable Implemenetation

    public override IEnumerator ApplyEffect(UnitStatusEffectSO effect) => unitEffects.ApplyEffect(effect);

    #endregion
}
