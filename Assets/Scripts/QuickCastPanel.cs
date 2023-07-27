using System.Collections.Generic;
using UnityEngine;

public class QuickCastPanel : MonoBehaviour
{
    private const int MAX_QUICK_CASTS = 9;

    [SerializeField] private UnitPicker unitPicker;
    [SerializeField] private Transform quickCastPrefab;
    [SerializeField] private BattlePlayer player;
    [SerializeField] private PickedActionsPanel pickedActionsPanel;

    private readonly List<QuickCast> quickCasts = new List<QuickCast>();

    private UnitActionScriptableObject lastClickedUnitActionScriptableObject;

    private void Start()
    {
        ClearQuickCastPanel();
        SetupQuickCasts();

        unitPicker.UnitsPicked += OnUnitsPicked;
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
    }

    private void OnQuickCastClicked(UnitActionScriptableObject scriptableObject)
    {
        lastClickedUnitActionScriptableObject = scriptableObject;
        unitPicker.StartPicking();
    }

    private void OnUnitsPicked(PickedUnitsEventArgs obj)
    {
        // todo: implement mechanism / factory to create unit actions
        var unitAction = new AttackUnitAction(lastClickedUnitActionScriptableObject, player, obj.PickedUnits[0]);

        pickedActionsPanel.SetupPickedAction(unitAction);
    }
}
