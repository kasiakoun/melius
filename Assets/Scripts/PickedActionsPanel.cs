using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickedActionsPanel : MonoBehaviour
{
    private const int MAX_ACTIONS_COUNT = 3;

    [SerializeField] private Transform pickedActionPrefab;
    [SerializeField] private Transform pickedActionsContainer;
    [SerializeField] private Transform playButton;
    [SerializeField] private BattleHandler battleHandler;

    private readonly List<PickedAction> pickedActions = new List<PickedAction>();

    private void Start()
    {
        foreach (Transform pickedAction in pickedActionsContainer)
        {
            Destroy(pickedAction.gameObject);
        }

        var offsetX = 0f;
        for (var i = 0; i < MAX_ACTIONS_COUNT; i++)
        {
            var pickedActionTransform = Instantiate(pickedActionPrefab);
            pickedActionTransform.SetParent(pickedActionsContainer);

            var pickedActionRectTransform = pickedActionTransform.GetComponent<RectTransform>();
            pickedActionRectTransform.anchoredPosition3D = new Vector3(offsetX, 0f, 0f);

            offsetX += pickedActionRectTransform.rect.width;

            var pickedAction = pickedActionTransform.GetComponent<PickedAction>();
            pickedActions.Add(pickedAction);
        }

        var playButtonRect = playButton.GetComponent<RectTransform>();
        playButtonRect.anchoredPosition3D = new Vector3(offsetX, 0f, 0f);
    }

    public void PlayActions()
    {
        var unitActions = pickedActions
            .Where(p => p.UnitAction != null)
            .Select(p => p.UnitAction)
            .ToList();
        battleHandler.Handle(unitActions);
    }

    public void SetupPickedAction(IUnitAction unitAction)
    {
        var pickedAction = pickedActions.FirstOrDefault(p => p.IsCleared);
        if (pickedAction == null) return;

        pickedAction.SetupAction(unitAction);
    }
}
