using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private TMP_Text scoreText, gameOverBestScore, gameOverCoinsColleced, bossName;
    [SerializeField] private GameObject bossHealthBar, boss;
    [SerializeField] private BonusesManager bonusesManager;
    [SerializeField] private Image bossHealthFill;
    private float spawnTime = 3;
    private bool spawningObjects = true;
    private const float spawnFrame = 2.6f;
    private int score, coinsCollected, bossCount;

    private void Start()
    {
        StartCoroutine(ScoreAdd());
    }

    private IEnumerator ScoreAdd()
    {
        WaitForSeconds second = new(0.1f);
        while (spawningObjects)
        {
            score++;
            scoreText.text = score.ToString("D9");
            if (score % 1000 == 0)
            {
                StartCoroutine(BossFight());
            }
            yield return second;
        }
    }

    private IEnumerator BossFight()
    {
        bossCount++;
        bossName.text = "Boss - " + bossCount;
        bonusesManager.enabled = false;
        bossHealthBar.SetActive(true);
        spawningObjects = false;
        Vector2 spawnPosition = new Vector2(16, 0);
        DestroyableObject bossObject = Instantiate(boss, spawnPosition, Quaternion.identity).GetComponent<DestroyableObject>();
        bossObject.transform.DOMoveX(5.5f, 1);
        bossObject.SetHealthBar(bossHealthFill);

        while (bossObject != null)
        {
            yield return null;
        }

        spawningObjects = true;
        StartCoroutine(ScoreAdd());
        bossHealthBar.SetActive(false);
        bonusesManager.enabled = true;

        yield break;
    }

    private void Update()
    {
        if (spawningObjects == false) return;

        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)
        {
            GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
            Vector2 spawnPosition = new Vector2(16, Random.Range(-spawnFrame, spawnFrame));

            Instantiate(randomObject, spawnPosition, Quaternion.identity);

            spawnTime = Random.Range(0.5f, 1.25f);
        }
    }
    public void CoinCollected(int count) => coinsCollected += count;

    public void GameOver()
    {
        if (PlayerPrefs.GetString("Vibration", "On") == "On") Handheld.Vibrate();

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        gameOverBestScore.text = bestScore.ToString();
        gameOverCoinsColleced.text = coinsCollected.ToString();

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        int deaths = PlayerPrefs.GetInt("Deaths", 0);
        deaths++;
        PlayerPrefs.SetInt("Deaths", deaths);

        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Unpause()
    {
        Time.timeScale = 1;
    }
}