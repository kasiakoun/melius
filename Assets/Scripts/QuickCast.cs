using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickCast : MonoBehaviour
{
    [SerializeField] private UnitActionScriptableObject unitActionScriptableObject;
    [SerializeField] private Image actionImage;

    private Button button;

    public event Action<UnitActionScriptableObject> ButtonClicked;

    public UnitActionScriptableObject UnitActionScriptableObject
    {
        get => unitActionScriptableObject;
        set
        {
            unitActionScriptableObject = value;
            if (value != null)
            {
                actionImage.sprite = value.icon;
                actionImage.enabled = true;
            }
            else
            {
                actionImage.enabled = false;
                actionImage.sprite = null;
            }
        }
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (unitActionScriptableObject == null) return;

        ButtonClicked?.Invoke(unitActionScriptableObject);
    }

    public void Hide() => gameObject.SetActive(false);
    public void Show() => gameObject.SetActive(true);
}
