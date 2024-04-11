using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    private IEnumerator Start()
    {
        WaitForSeconds fourSecondsWait = new WaitForSeconds(4);
        WaitForSeconds halfSecondWait = new WaitForSeconds(0.5f);

        while (true)
        {
            yield return fourSecondsWait;

            for (int i = 0; i < 3; i++)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                yield return halfSecondWait;
            }

        }
    }
    private void OnDestroy()
    {
        AddKillStats();
    }
    private void AddKillStats()
    {
        int totalBoses = PlayerPrefs.GetInt("TotalBosses", 0);
        totalBoses++;
        PlayerPrefs.SetInt("TotalBosses", totalBoses);
    }
}