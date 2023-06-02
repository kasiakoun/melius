using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform playerProgressBarUI;

    private void LateUpdate()
    {
        playerProgressBarUI.forward = Camera.main.transform.forward;
    }
}
