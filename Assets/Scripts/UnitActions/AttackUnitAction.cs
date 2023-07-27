using System.Collections;

public class AttackUnitAction : UnitAction
{
    private IBattleUnit owner;
    private IBattleUnit target;

    public IBattleUnit TargetUnit => target;

    public AttackUnitAction(UnitActionScriptableObject scriptableObject,
        IBattleUnit owner,
        IBattleUnit target)
        : base(scriptableObject)
    {
        this.owner = owner;
        this.target = target;
    }

    public override IEnumerator MakeAction()
    {
        yield return owner.Move(target.Position);
        yield return owner.Rotate(target.Position);
        yield return owner.Attack();
        target.TakeDamage();
    }
}
