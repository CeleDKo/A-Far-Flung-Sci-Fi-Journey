using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareUrl : MonoBehaviour
{
    public void Share(string url)
    {
        new NativeShare().SetText("Share App").SetUrl(url).Share();
    }
}