public class UnitStatusEffect
{
    public UnitStatusEffectSO UnitStatusEffectSO { get; private set; }

    public int DurationLeft { get; private set; }

    public UnitStatusEffect(UnitStatusEffectSO unitStatusEffectSo)
    {
        UnitStatusEffectSO = unitStatusEffectSo;
        DurationLeft = unitStatusEffectSo.Duration;
    }

    public void DecreaseDuration()
    {
        DurationLeft--;
    }
}
