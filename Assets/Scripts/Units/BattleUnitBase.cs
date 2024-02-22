using System.Collections;
using UnityEngine;

public abstract class BattleUnitBase : MonoBehaviour, IEffectable
{
    public abstract Vector3 Position { get; }
    public abstract Vector3 TargetPosition { get; }
    public abstract UnitScriptableObject ScriptableObject { get; }
    public abstract float StopDistanceToAttack { get; }
    public abstract IEnumerator Move(Vector3 destination, float stopDistance = 1.0f);
    public abstract IEnumerator Rotate(Vector3 unit);
    public abstract void TakeDamage();
    public abstract IEnumerator Attack(BattleUnitBase targetBattleUnit);
    public abstract bool IsMelee();
    public abstract void SetHighlightOutline(bool enable);
    public abstract IEnumerator ApplyEffect(UnitStatusEffectSO effect);
}
