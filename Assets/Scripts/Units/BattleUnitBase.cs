using System.Collections;
using UnityEngine;

public abstract class BattleUnitBase : MonoBehaviour
{
    public abstract Vector3 Position { get; }
    public abstract Vector3 TargetPosition { get; }
    public abstract UnitScriptableObject ScriptableObject { get; }
    public abstract IEnumerator Move(Vector3 destination);
    public abstract IEnumerator Rotate(Vector3 unit);
    public abstract void TakeDamage();
    public abstract IEnumerator Attack(BattleUnitBase targetBattleUnit);
    public abstract void SetHighlightOutline(bool enable);
}
