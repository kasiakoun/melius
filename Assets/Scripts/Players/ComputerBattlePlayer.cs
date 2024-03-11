using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComputerBattlePlayer : MonoBehaviour, IBattleTurnPlayer
{
    [SerializeField] private BattleUnit battleUnit;
    [SerializeField] private BattleHandler battleHandler;
    [SerializeField] UnitDeath unitDeath;
    // todo: temp solution to test attack by computer
    [SerializeField] private UnitActionComposite attackUnitActionComposite;

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
    public BattleUnitBase BattleUnit => battleUnit;

    public void MakeTurn()
    {
        //Debug.Log("ComputerBattlePlayer: MakeTurn");
        //// todo: should replace all this code with something specific
        //// todo: it's a temp solution to show how it works
        var test = Object.FindObjectsByType<PlayerBattleUnit>(FindObjectsSortMode.None);
        var enemyTarget = test[0];

        var unitActions = new List<IUnitAction>();
        var unitActionParameters = new UnitActionParameters.UnitActionParametersBuilder()
            .SetScriptableObject(attackUnitActionComposite.scriptableObject)
            .SetOwner(battleUnit)
            .SetTarget(enemyTarget)
            .Build();
        if (attackUnitActionComposite.validator.CanAction(unitActionParameters))
        {
            unitActions.Add(attackUnitActionComposite.factory.CreateUnitAction(unitActionParameters));
        }

        //battleHandler.Handle(this, unitActions);
        StartCoroutine(HandleActions(unitActions));
    }

    public IEnumerator HandleActions(List<IUnitAction> unitActions)
    {
        yield return new WaitForSeconds(0.5f);
        battleHandler.Handle(this, unitActions);
    }

    #endregion
}
