using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(UnitAttacking))]
[RequireComponent(typeof(UnitDamageable))]
[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(UnitRotation))]
public class BattleUnit : MonoBehaviour, IBattleUnit
{
    [SerializeField] private UnitScriptableObject scriptableObject;

    private NavMeshAgent navMeshAgent;
    private Outline outline;
    private UnitAttacking unitAttacking;
    private UnitDamageable unitDamageable;
    private UnitMovement unitMovement;
    private UnitRotation unitRotation;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        outline = GetComponent<Outline>();
        unitAttacking = GetComponent<UnitAttacking>();
        unitDamageable = GetComponent<UnitDamageable>();
        unitMovement = GetComponent<UnitMovement>();
        unitRotation = GetComponent<UnitRotation>();
    }

    public bool IsWalking() => unitMovement.IsWalking;

    #region IBattleUnit Implementation

    public Vector3 Position => navMeshAgent.destination;
    public UnitScriptableObject ScriptableObject => scriptableObject;

    public IEnumerator Move(Vector3 destination) => unitMovement.Move(destination);

    public IEnumerator Rotate(Vector3 position) => unitRotation.Rotate(position);

    public void TakeDamage() => unitDamageable.TakeDamage();

    public IEnumerator Attack() => unitAttacking.Attack();

    public void SetHighlightOutline(bool enable) => outline.enabled = enable;

    #endregion
}
