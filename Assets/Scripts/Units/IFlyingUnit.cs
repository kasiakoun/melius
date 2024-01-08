using System.Collections;
using UnityEngine;

public interface IFlyingUnit
{
    Transform BaseHolder { get; }
    IEnumerator ActivateFlying();
}
