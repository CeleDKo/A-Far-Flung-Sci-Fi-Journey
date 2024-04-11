using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOrientation : MonoBehaviour
{
    [SerializeField] private ScreenOrientation screenOrientation;
    private void Awake()
    {
        Screen.orientation = screenOrientation;
    }
}
