using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickCast : MonoBehaviour
{
    [SerializeField] private UnitActionComposite unitActionComposite;
    [SerializeField] private Image actionImage;

    private Button button;

    public event Action<UnitActionComposite> ButtonClicked;

    public UnitActionComposite UnitActionComposite
    {
        get => unitActionComposite;
        set
        {
            unitActionComposite = value;
            if (value != null)
            {
                actionImage.sprite = value.scriptableObject.icon;
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
        if (unitActionComposite == null) return;

        ButtonClicked?.Invoke(unitActionComposite);
    }

    public void Hide() => gameObject.SetActive(false);
    public void Show() => gameObject.SetActive(true);
}
