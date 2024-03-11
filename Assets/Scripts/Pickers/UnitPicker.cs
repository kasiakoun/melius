using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickedUnitsEventArgs : EventArgs
{
    public List<BattleUnitBase> PickedUnits { get; set; }
}

public class UnitPicker : WaitingPicker
{
    [SerializeField] private LayerMask unitLayerMask;

    private List<BattleUnitBase> battleUnits;

    private bool isPicking;
    private UnitActionValidator pickedActionValidator;
    private BattleUnitBase pickedBattleUnit;

    public event Action<PickedUnitsEventArgs> UnitsPicked;

    public void Start()
    {
        var battleUnits = FindObjectsByType<BattleUnit>(FindObjectsSortMode.None);
        this.battleUnits = battleUnits.Cast<BattleUnitBase>().ToList();
    }

    public bool HandleLeftClick(Vector3 vector)
    {
        if (!isPicking) return false;

        var ray = Camera.main.ScreenPointToRay(vector);
        var battleUnit = GetBattleUnitByRay(ray);
        if (battleUnit == null) return false;

        var unitActionParameters = new UnitActionParameters.UnitActionParametersBuilder()
            .SetOwner(pickedBattleUnit)
            .SetTarget(battleUnit)
            .Build();
        if (!pickedActionValidator.CanAction(unitActionParameters)) return false;

        UnitsPicked?.Invoke(new PickedUnitsEventArgs()
        {
            PickedUnits = new List<BattleUnitBase> { battleUnit }
        });
        StopPicking();

        return true;
    }

    private BattleUnitBase GetBattleUnitByRay(Ray ray)
    {
        var maxDistance = 1000f;
        if (!Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, unitLayerMask))
        {
            return null;
        }

        return raycastHit.transform.GetComponent<BattleUnit>();
    }

    public override void StartPicking(UnitActionValidator validator, BattleUnitBase pickerBattleUnit)
    {
        isPicking = true;
        pickedActionValidator = validator;
        pickedBattleUnit = pickerBattleUnit;
        foreach (var battleUnit in battleUnits)
        {
            var unitActionParameters = new UnitActionParameters.UnitActionParametersBuilder()
                .SetOwner(pickedBattleUnit)
                .SetTarget(battleUnit)
                .Build();
            if (!validator.CanAction(unitActionParameters)) continue;

            battleUnit.SetHighlightOutline(true);
        }
    }

    public void StopPicking()
    {
        isPicking = false;
        foreach (var battleUnit in battleUnits)
        {
            battleUnit.SetHighlightOutline(false);
        }
        pickedActionValidator = null;
        pickedBattleUnit = null;
    }
}
