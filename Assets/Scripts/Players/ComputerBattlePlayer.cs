using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComputerBattlePlayer : MonoBehaviour, IBattleTurnPlayer
{
    [SerializeField] private BattleUnit battleUnit;
    [SerializeField] private BattleHandler battleHandler;
    [SerializeField] UnitDeath unitDeath;

    private void Start()
    {
        unitDeath.UnitDied += OnUnitDied;
    }

    private void OnDestroy()
    {
        unitDeath.UnitDied -= OnUnitDied;
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
        Debug.Log("ComputerBattlePlayer: MakeTurn");
        // todo: should replace all this code with something specific
        // todo: it's a temp solution to show how it works
        var unitActionScriptableObjects = Resources.FindObjectsOfTypeAll<UnitActionScriptableObject>();
        var attackActionScriptableObject =
            unitActionScriptableObjects.FirstOrDefault(p => p.unityActionType.StoredType.Name == "AttackUnitAction");

        var enemyTarget = Resources.FindObjectsOfTypeAll<PlayerBattleUnit>()[0];

        var attackAction = new AttackUnitAction(attackActionScriptableObject, battleUnit, enemyTarget);
        battleHandler.Handle(this, new List<IUnitAction> { attackAction });
    }

    #endregion
}
