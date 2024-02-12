using System.Collections;

public class AttackUnitAction : UnitAction
{
    private BattleUnitBase owner;
    private BattleUnitBase target;

    public BattleUnitBase TargetUnit => target;

    public AttackUnitAction(UnitActionScriptableObject scriptableObject,
        BattleUnitBase owner,
        BattleUnitBase target)
        : base(scriptableObject)
    {
        this.owner = owner;
        this.target = target;
    }

    public override IEnumerator MakeAction()
    {
        // todo: should we move / rotate when attacks is melee / range
        //yield return owner.Move(target.Position);
        yield return owner.Rotate(target.Position);
        yield return owner.Attack(target);
        target.TakeDamage();
    }
}
