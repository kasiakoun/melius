using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private BowAnimator bowAnimator;

    public BowAnimator BowAnimator => bowAnimator;
}
