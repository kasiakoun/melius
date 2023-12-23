using UnityEngine;

public class PlayerAnimator : BaseUnitAnimator
{
    private const string IS_WALKING = "IsWalking";
    private const string RIGHT_MELLEE_ATTACK = "RightMeleeAttack";
    private const string TAKE_DAMAGE = "TakeDamage";
    private const string BOW_SHOT = "BowShot";
    
    [SerializeField] private PlayerBattleUnit player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

    public void RightMeleeAttack()
    {
        animator.SetTrigger(RIGHT_MELLEE_ATTACK);
    }

    public override void TakeDamage()
    {
        animator.SetTrigger(TAKE_DAMAGE);
    }

    public override void SimpleAttack()
    {
        animator.SetTrigger(RIGHT_MELLEE_ATTACK);
    }

    public void BowShot()
    {
        animator.SetTrigger(BOW_SHOT);
    }
}