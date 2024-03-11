using System;
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
[RequireComponent(typeof(UnitEffects))]
public class BattleUnit : BattleUnitBase
{
    [SerializeField] private UnitScriptableObject scriptableObject;

    private NavMeshAgent navMeshAgent;
    private Outline outline;
    private UnitAttacking unitAttacking;
    private UnitDamageable unitDamageable;
    private UnitMovement unitMovement;
    private UnitRotation unitRotation;
    private UnitHealth unitHealth;
    private UnitEffects unitEffects;

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
        unitEffects = GetComponent<UnitEffects>();
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

    #region IEffectable Implemenetation

    public override IEnumerator ApplyEffect(UnitStatusEffectSO effect) => unitEffects.ApplyEffect(effect);

    #endregion
}
