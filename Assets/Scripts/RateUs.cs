using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class RateUs : MonoBehaviour
{
    public void ExecuteRate() => Device.RequestStoreReview();
}