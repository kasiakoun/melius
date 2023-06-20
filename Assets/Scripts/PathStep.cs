using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PathStep : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    public void OnTriggerEnter(Collider other)
    {
        if (playerLayer != (playerLayer | (1 << other.gameObject.layer))) return;

        Destroy(gameObject);
    }
}
