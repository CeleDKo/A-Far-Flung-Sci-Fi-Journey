using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuStatistics : MonoBehaviour
{
    [SerializeField] private TMP_Text bestScoreText, totalCoinsText, totalKillsText, totalBosesText, totalDeathsText;

    private void OnEnable()
    {
        bestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
        totalCoinsText.text = PlayerPrefs.GetInt("TotalCoins").ToString();
        totalKillsText.text = PlayerPrefs.GetInt("Kills").ToString();
        totalBosesText.text = PlayerPrefs.GetInt("TotalBosses").ToString();
        totalDeathsText.text = PlayerPrefs.GetInt("Deaths").ToString();
    }
}