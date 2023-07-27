using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickedUnitsEventArgs : EventArgs
{
    public List<IBattleUnit> PickedUnits { get; set; }
}

public class UnitPicker : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask unitLayerMask;

    private List<IBattleUnit> battleUnits;

    private bool isPicking;

    public event Action<PickedUnitsEventArgs> UnitsPicked;

    public void Start()
    {
        var battleUnits = FindObjectsByType<BattleUnit>(FindObjectsSortMode.None);
        this.battleUnits = battleUnits.Cast<IBattleUnit>().ToList();

        gameInput.PlayerClicked += OnPlayerClicked;
    }

    private void OnPlayerClicked(Vector3 vector)
    {
        if (!isPicking) return;

        var ray = Camera.main.ScreenPointToRay(vector);
        var battleUnit = GetBattleUnitByRay(ray);
        if (battleUnit == null) return;

        UnitsPicked?.Invoke(new PickedUnitsEventArgs()
        {
            PickedUnits = new List<IBattleUnit> { battleUnit }
        });
        StopPicking();
    }

    private IBattleUnit GetBattleUnitByRay(Ray ray)
    {
        var maxDistance = 1000f;
        if (!Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, unitLayerMask))
        {
            return null;
        }

        return raycastHit.transform.GetComponent<BattleUnit>();
    }

    public void StartPicking()
    {
        isPicking = true;
        foreach (var battleUnit in battleUnits)
        {
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
    }
}
