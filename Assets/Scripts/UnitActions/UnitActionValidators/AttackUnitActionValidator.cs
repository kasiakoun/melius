public class AttackUnitActionValidator : UnitActionValidator
{
    public override bool CanAction(UnitActionParameters parameters)
    {
        return false;
    }
}
