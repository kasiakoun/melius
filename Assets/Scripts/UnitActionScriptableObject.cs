using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitActionScriptableObject : ScriptableObject
{
    public Sprite icon;
    public InspectableType<UnitAction> unityActionType;
}
