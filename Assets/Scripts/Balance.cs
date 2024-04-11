using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class Balance : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private int balance;
    public int CurrentBalance => balance;
    private void Start()
    {
        balance = PlayerPrefs.GetInt("Balance", 0);
        text.text = balance.ToString();
    }
    public void Add(int count)
    {
        balance += count;

        int totalCoins = PlayerPrefs.GetInt("TotalCoins");
        if (balance > totalCoins)
            PlayerPrefs.SetInt("TotalCoins", balance);

        text.text = balance.ToString();
        PlayerPrefs.SetInt("Balance", balance);
    }
    public void Minus(int count)
    {
        if (balance - count < 0) count = balance;

        balance -= count;
        text.text = balance.ToString();
        PlayerPrefs.SetInt("Balance", balance);
    }
}