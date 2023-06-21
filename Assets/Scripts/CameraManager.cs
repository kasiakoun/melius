using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerVirtualCamera;
    [SerializeField] CinemachineVirtualCamera worldVirtualCamera;

    public void SwitchToPlayerCamera()
    {
        worldVirtualCamera.Priority = 10;
        playerVirtualCamera.Priority = 100;
    }

    public void SwitchToWorldCamera()
    {
        worldVirtualCamera.Priority = 100;
        playerVirtualCamera.Priority = 10;
    }
}
