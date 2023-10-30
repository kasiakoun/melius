using UnityEngine;

public class BattlePlayer : MonoBehaviour, IBattleTurnPlayer
{
    [SerializeField] private PlayerBattleUnit battleUnit;
    //[SerializeField] private UnitDeath unitDeath;

    private void Start()
    {
        //unitDeath.UnitDied += OnUnitDied;
    }

    private void OnDestroy()
    {
        //unitDeath.UnitDied -= OnUnitDied;
    }

    private void OnUnitDied()
    {
        UnitIsDead = true;
    }

    #region IBattlePlayerTurn Implementation

    public bool UnitIsDead { get; private set; }
    public IBattleUnit BattleUnit => battleUnit;

    public void MakeTurn()
    {
        Debug.Log("BattlePlayer: MakeTurn");
        //battleUI.Show();
    }

    #endregion
}
