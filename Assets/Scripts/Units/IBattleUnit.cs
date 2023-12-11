using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IBattleUnit
{
    Vector3 Position { get; }
    Vector3 TargetPosition { get; }
    UnitScriptableObject ScriptableObject { get; }
    IEnumerator Move(Vector3 destination);
    IEnumerator Rotate(Vector3 unit);
    void TakeDamage();
    IEnumerator Attack(IBattleUnit targetBattleUnit);
    void SetHighlightOutline(bool enable);
}
