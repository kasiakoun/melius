using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitActionFactory
{
    IUnitAction CreateUnitAction(UnitActionParameters unitActionParameters);
}
