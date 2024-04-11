using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    [SerializeField] private Balance balance;
    public void ExecuteResetProgress()
    {
        balance.Minus(balance.CurrentBalance);
        PlayerPrefs.DeleteAll();
    }
}