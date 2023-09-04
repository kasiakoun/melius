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
        var type = unitActionParameters.TypeAction;
        var parameters = unitActionParameters.Parameters.ToArray();

        return Activator.CreateInstance(type, parameters) as IUnitAction;
    }
}
