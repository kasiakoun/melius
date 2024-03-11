using System.Collections;

public interface IUnitAction
{
    UnitActionScriptableObject ScriptableObject { get; }
    IEnumerator MakeAction();
}
