using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public abstract class UnitStatusEffectSO : ScriptableObject
{
    [SerializeField] public abstract int Duration { get; }
    public abstract IEnumerator ApplyEffect(BattleUnitBase unit);
    public abstract IEnumerator RemoveEffect(BattleUnitBase unit);
}
