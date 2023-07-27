using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAction : IUnitAction
{
    protected UnitAction(UnitActionScriptableObject scriptableObject)
    {
        ScriptableObject = scriptableObject;
    }

    #region IUnitAction Implementation

    public UnitActionScriptableObject ScriptableObject { get; private set; }

    public abstract IEnumerator MakeAction();

    #endregion
}
