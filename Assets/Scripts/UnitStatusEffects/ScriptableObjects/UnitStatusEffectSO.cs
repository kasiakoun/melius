using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public abstract class UnitStatusEffectSO : ScriptableObject
{
    [SerializeField] public int Duration { get; private set; }
    public abstract IEnumerator ApplyEffect(BattleUnitBase unit);
    public abstract IEnumerator RemoveEffect(BattleUnitBase unit);
}
