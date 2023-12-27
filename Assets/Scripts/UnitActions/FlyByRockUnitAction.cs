using System.Collections;

public class FlyByRockUnitAction : UnitAction
{
    private readonly IBattleUnit unit;
    private readonly EnhancedObject enhancedObject;

    public FlyByRockUnitAction(UnitActionScriptableObject scriptableObject,
        IBattleUnit unit,
        EnhancedObject enhancedObject)
        : base(scriptableObject)
    {
        this.unit = unit;
        this.enhancedObject = enhancedObject;
    }

    public override IEnumerator MakeAction()
    {
        yield break;
    }
}
