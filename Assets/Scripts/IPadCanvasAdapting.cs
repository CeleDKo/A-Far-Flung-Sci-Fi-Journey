using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IPadCanvasAdapting : MonoBehaviour
{
    [SerializeField] private CanvasScaler scaler;
    [SerializeField] private float match;
    private void Awake()
    {
        if (UnityEngine.iOS.Device.generation.ToString().Contains("iPad"))
        {
            scaler.matchWidthOrHeight = match;
        }
    }
}