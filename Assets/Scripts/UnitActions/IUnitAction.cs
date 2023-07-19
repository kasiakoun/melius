using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitAction
{
    public UnitActionScriptableObject ScriptableObject { get; }
    public void MakeAction();
}
