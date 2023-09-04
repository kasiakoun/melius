using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionParameters
{
    public Type TypeAction { get; private set; }
    public UnitActionScriptableObject ScriptableObject { get; private set; }
    public List<object> Parameters { get; private set; }

    public UnitActionParameters(UnitActionScriptableObject scriptableObject, List<object> parameters)
    {
        TypeAction = scriptableObject.unityActionType.StoredType;
        ScriptableObject = scriptableObject;
        parameters.Insert(0, scriptableObject);
        Parameters = parameters;
    }
}
