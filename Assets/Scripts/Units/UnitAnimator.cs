using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private const string TAKE_DAMAGE = "TakeDamage";
    private const string ATTACK = "Attack";
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private BattleUnit unit;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, unit.IsWalking());
    }

    public void TakeDamage()
    {
        animator.SetTrigger(TAKE_DAMAGE);
    }

    public void Attack()
    {
        animator.SetTrigger(ATTACK);
    }
}
