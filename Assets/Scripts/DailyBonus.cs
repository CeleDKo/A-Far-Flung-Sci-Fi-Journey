using System;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonus : MonoBehaviour
{
    [SerializeField] private Button dailyBonusButton;
    private bool dayIsCompleted;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("DailyReward"))
        {
            string rewardClaimDateString = PlayerPrefs.GetString("DailyReward");
            Debug.Log("RewardClaimDate - " + rewardClaimDateString);
            DateTime rewardClaimDate = DateTime.ParseExact(rewardClaimDateString, "yyyy/M/d/H/m", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            DateTime dateTimeNow = DateTime.UtcNow;

            dayIsCompleted = dateTimeNow.Year >= rewardClaimDate.Year && dateTimeNow.Month >= rewardClaimDate.Month
                && dateTimeNow.Day >= rewardClaimDate.Day + 1 && dateTimeNow.Hour >= rewardClaimDate.Hour && dateTimeNow.Minute >= rewardClaimDate.Minute;

            Debug.Log("DateTimeNow - " + dateTimeNow);
            Debug.Log("DayIsCompleted - " + dayIsCompleted);
        }
        else dayIsCompleted = true;

        dailyBonusButton.interactable = dayIsCompleted;
    }
    public void ClaimDailyReward()
    {
        if (dayIsCompleted == false) return;

        DateTime dateTimeNow = DateTime.UtcNow;
        string dateTimeNowString = $"{dateTimeNow.Year}/{dateTimeNow.Month}/{dateTimeNow.Day}/{dateTimeNow.Hour}/{dateTimeNow.Minute}";
        Debug.Log(dateTimeNowString);
        PlayerPrefs.SetString("DailyReward", dateTimeNowString);

        dayIsCompleted = false;
        dailyBonusButton.interactable = false;
    }
}