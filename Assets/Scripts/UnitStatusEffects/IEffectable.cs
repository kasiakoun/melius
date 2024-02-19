using System.Collections;

public interface IEffectable
{
    IEnumerator ApplyEffect(UnitStatusEffectSO effect);
}
