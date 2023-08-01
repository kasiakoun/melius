using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class UnitActionFactory : IUnitActionFactory
{
    private static UnitActionFactory _instance;

    public static IUnitActionFactory Instance => _instance ??= new UnitActionFactory();

    private UnitActionFactory()
    {
    }

    public IUnitAction CreateUnitAction(UnitActionParameters unitActionParameters)
    {
        var scriptableObject = unitActionParameters.scriptableObject;
        var type = scriptableObject.unityActionType.StoredType;

        var parameters = new object[] { scriptableObject, unitActionParameters.owner, unitActionParameters.target };

        return Activator.CreateInstance(type, parameters) as IUnitAction;
    }
}
