using System;
using UnityEngine;

public abstract class UnitActionWithStatusEffect<TStatusEffect> : UnitAction where TStatusEffect : UnitStatusEffectSO
{
    protected TStatusEffect UnitStatusEffectSO { get; private set; }

    protected UnitActionWithStatusEffect(UnitActionScriptableObject scriptableObject)
        : base(scriptableObject)
    {
        UnitStatusEffectSO = UnityEngine.ScriptableObject.CreateInstance<TStatusEffect>();
    }
}
