using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    void Start()
    {
        Player.Instance.SelectedCounterChanged += OnSelectedCounterChanged;
    }

    private void OnSelectedCounterChanged(BaseCounter obj)
    {
        if (baseCounter == obj)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
