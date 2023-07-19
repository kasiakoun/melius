using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAction : MonoBehaviour, IUnitAction
{
    [SerializeField] private UnitActionScriptableObject scriptableObject;

    #region IUnitAction Implementation

    public UnitActionScriptableObject ScriptableObject => scriptableObject;

    public virtual void MakeAction()
    {
        Debug.Log("MakeAction is not implemented");
        throw new NotImplementedException();
    }

    #endregion
}
