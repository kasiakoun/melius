using System.Collections;
using UnityEngine;

public interface IFlyingUnit
{
    Transform BaseHolder { get; }
    bool IsFlying { get; }
    IEnumerator ActivateFlying();
    IEnumerator DeactivateFlying();
}
