using System.Collections.Generic;
using UnityEngine;

public class QuickCastPanel : MonoBehaviour
{
    private const int MAX_QUICK_CASTS = 9;

    // todo: replace unitPicker / positionPicker
    [SerializeField] private UnitPicker unitPicker;
    [SerializeField] private PositionPicker positionPicker;

    [SerializeField] private Transform quickCastPrefab;
    [SerializeField] private BattlePlayer player;
    [SerializeField] private PickedActionsPanel pickedActionsPanel;

    // todo: temp field
    [SerializeField] private UnitActionScriptableObject moveUnitActionScriptableObject;
    [SerializeField] private UnitActionScriptableObject attackUnitActionScriptableObject;

    private readonly IUnitActionFactory unitActionFactory = UnitActionFactory.Instance;

    private readonly List<QuickCast> quickCasts = new List<QuickCast>();

    private UnitActionScriptableObject lastClickedUnitActionScriptableObject;

    private void Start()
    {
        ClearQuickCastPanel();
        SetupQuickCasts();

        unitPicker.UnitsPicked += OnUnitsPicked;
        positionPicker.PositionPicked += OnPositionPicked;
    }

    private void ClearQuickCastPanel()
    {
        foreach (Transform quickCast in transform)
        {
            Destroy(quickCast.gameObject);
        }
    }

    private void SetupQuickCasts()
    {
        var offsetX = 0f;
        for (var i = 0; i < MAX_QUICK_CASTS; i++)
        {
            var quickCastTransform = Instantiate(quickCastPrefab);
            quickCastTransform.SetParent(transform);

            var quickCastRectTransform = quickCastTransform.GetComponent<RectTransform>();
            quickCastRectTransform.anchoredPosition3D = new Vector3(offsetX, 0f, 0f);

            offsetX += quickCastRectTransform.rect.width;

            var quickCast = quickCastTransform.GetComponent<QuickCast>();
            quickCast.ButtonClicked += OnQuickCastClicked;
            quickCasts.Add(quickCast);
        }

        // todo: temp init
        quickCasts[0].UnitActionScriptableObject = moveUnitActionScriptableObject;
        quickCasts[1].UnitActionScriptableObject = attackUnitActionScriptableObject;
    }

    private void OnQuickCastClicked(UnitActionScriptableObject scriptableObject)
    {
        // todo: replace with something universal(unitPicker / positionPicker)
        lastClickedUnitActionScriptableObject = scriptableObject;
        var unitActionType = scriptableObject.unityActionType.StoredType;
        if (unitActionType == typeof(AttackUnitAction))
        {
            unitPicker.StartPicking();
        }
        else if (unitActionType == typeof(MoveUnitAction))
        {
            positionPicker.StartPicking();
        }
    }

    private void OnPositionPicked(Vector3 obj)
    {
        var unitActionParameters = new UnitActionParameters(
            lastClickedUnitActionScriptableObject,
            new List<object>
            {
                player,
                obj,
            });
        var unitAction = unitActionFactory.CreateUnitAction(unitActionParameters);

        pickedActionsPanel.SetupPickedAction(unitAction);
    }

    private void OnUnitsPicked(PickedUnitsEventArgs obj)
    {
        var unitActionParameters = new UnitActionParameters(
            lastClickedUnitActionScriptableObject,
            new List<object>
            {
                player,
                obj.PickedUnits[0],
            });
        var unitAction = unitActionFactory.CreateUnitAction(unitActionParameters);

        pickedActionsPanel.SetupPickedAction(unitAction);
    }
}