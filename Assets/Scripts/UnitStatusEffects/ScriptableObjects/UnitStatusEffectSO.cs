using UnityEngine;

[CreateAssetMenu]
public abstract class UnitStatusEffectSO : ScriptableObject
{
    [SerializeField] public int Duration { get; private set; }
    public abstract void ApplyEffect(BattleUnitBase unit);
}
