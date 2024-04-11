using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PageChange : MonoBehaviour
{
    [SerializeField] private CanvasGroup currentPage;
    private const float actionTime = 0.2f;
    private bool isChangingPage;
    private void Start()
    {
        if (Time.timeScale != 1) Time.timeScale = 1;
    }
    public void OpenPage(CanvasGroup neededPage)
    {
        StartCoroutine(OpeningPage(neededPage));
    }
    public void Play() => SceneManager.LoadScene("Game");
    private IEnumerator OpeningPage(CanvasGroup neededPage)
    {
        if (isChangingPage) yield break;

        isChangingPage = true;
        yield return currentPage.DOFade(0, actionTime).WaitForCompletion();
        currentPage.gameObject.SetActive(false);
        neededPage.alpha = 0;
        neededPage.gameObject.SetActive(true);
        yield return neededPage.DOFade(1, actionTime).WaitForCompletion();
        currentPage = neededPage;
        isChangingPage = false;
    }
    public void Menu() => SceneManager.LoadScene("Menu");
    public void Restart() => SceneManager.LoadScene("Game");
    public void Quit() => Application.Quit();
}