using System.Collections.Generic;
using UnityEngine;

public class QuickCastPanel : MonoBehaviour
{
    private const int MAX_QUICK_CASTS = 5;

    // todo: replace unitPicker / positionPicker / objectPicker
    [SerializeField] private UnitPicker unitPicker;
    [SerializeField] private PositionPicker positionPicker;
    [SerializeField] private ObjectPicker objectPicker;

    [SerializeField] private Transform quickCastPrefab;
    [SerializeField] private PlayerBattleUnit player;
    [SerializeField] private PickedActionsPanel pickedActionsPanel;

    // todo: temp field
    [SerializeField] private UnitActionComposite moveUnitActionComposite;
    [SerializeField] private UnitActionComposite attackUnitActionComposite;

    //private readonly IUnitActionFactory unitActionFactory = UnitActionFactory.Instance;

    private readonly List<QuickCast> quickCasts = new List<QuickCast>();

    private UnitActionComposite lastClickedUnitActionComposite;

    private void Start()
    {
        ClearQuickCastPanel();
        SetupQuickCasts();

        unitPicker.UnitsPicked += OnUnitsPicked;
        positionPicker.PositionPicked += OnPositionPicked;
        objectPicker.ObjectPicked += OnObjectPicked;
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
        quickCasts[0].UnitActionComposite = moveUnitActionComposite;
        quickCasts[1].UnitActionComposite = attackUnitActionComposite;
    }

    private void OnQuickCastClicked(UnitActionComposite unitActionComposite)
    {
        lastClickedUnitActionComposite = unitActionComposite;
        unitActionComposite.picker.StartPicking();
    }

    private void OnPositionPicked(Vector3 obj)
    {
        var unitActionParameters = new UnitActionParameters.UnitActionParametersBuilder()
            .SetScriptableObject(lastClickedUnitActionComposite.scriptableObject)
            .SetOwner(player)
            .SetPosition(obj)
            .Build();
        var unitAction = lastClickedUnitActionComposite.factory.CreateUnitAction(unitActionParameters);

        pickedActionsPanel.SetupPickedAction(unitAction);
    }

    private void OnUnitsPicked(PickedUnitsEventArgs obj)
    {
        var unitActionParameters = new UnitActionParameters.UnitActionParametersBuilder()
            .SetScriptableObject(lastClickedUnitActionComposite.scriptableObject)
            .SetOwner(player)
            .SetTarget(obj.PickedUnits[0])
            .Build();
        var unitAction = lastClickedUnitActionComposite.factory.CreateUnitAction(unitActionParameters);

        pickedActionsPanel.SetupPickedAction(unitAction);
    }

    private void OnObjectPicked(EnhancedObject obj)
    {
        var unitActionParameters = new UnitActionParameters.UnitActionParametersBuilder()
            .SetScriptableObject(obj.unitActionComposite.scriptableObject)
            .SetOwner(player)
            .SetEnhancedObject(obj)
            .Build();
        var unitAction = obj.unitActionComposite.factory.CreateUnitAction(unitActionParameters);

        pickedActionsPanel.SetupPickedAction(unitAction);
    }

    public void HidePanel() => quickCasts.ForEach(p => p.Hide());
    public void ShowPanel() => quickCasts.ForEach(p => p.Show());
}
