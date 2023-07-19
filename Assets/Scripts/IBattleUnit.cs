using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleUnit
{
    public IEnumerator Move(Vector3 destination);
    public IEnumerator Rotate(Transform unit);
    public void TakeDamage();
    public void Attack();
}
