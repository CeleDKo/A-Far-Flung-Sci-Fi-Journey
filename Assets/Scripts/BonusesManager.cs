using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusesManager : MonoBehaviour
{
    [SerializeField] private Bonus[] bonuses;
    [SerializeField] private CollectSystem playerCollectSystem;

    private void OnEnable()
    {
        foreach (Bonus bonus in bonuses)
        {
            bonus.Count = PlayerPrefs.GetInt(bonus.Type.ToString(), 0);
            bonus.CountText.text = bonus.Count.ToString();
        }
    }

    public void AddBonus(Type type)
    {
        foreach (Bonus bonus in bonuses)
        {
            if (bonus.Type != type) continue;

            bonus.Count = PlayerPrefs.GetInt(bonus.Type.ToString(), 0);
            bonus.Count++;
            PlayerPrefs.SetInt(bonus.Type.ToString(), bonus.Count);
            bonus.CountText.text = bonus.Count.ToString();
            break;
        }
    }
    public void MinusBonus(Type type)
    {
        foreach (Bonus bonus in bonuses)
        {
            if (bonus.Type != type) continue;

            bonus.Count = PlayerPrefs.GetInt(bonus.Type.ToString(), 0);
            bonus.Count--;
            PlayerPrefs.SetInt(bonus.Type.ToString(), bonus.Count);
            bonus.CountText.text = bonus.Count.ToString();
            break;
        }
    }

    private bool IsCanUseBonus(Type type)
    {
        foreach (Bonus bonus in bonuses)
        {
            if (bonus.Type != type) continue;

            return bonus.Count > 0;
        }
        return false;
    }

    public void ActivateCoinMultiplier()
    {
        if (IsCanUseBonus(Type.CoinMultiplier) == false || playerCollectSystem.IsMultiplier) return;

        MinusBonus(Type.CoinMultiplier);
        StartCoroutine(Reload(Type.CoinMultiplier, 10));
        playerCollectSystem.ActivateMultiplierCoin();
    }

    public void ActivateMagnite()
    {
        if (IsCanUseBonus(Type.Magnite) == false) return;

        MinusBonus(Type.Magnite);
        StartCoroutine(Reload(Type.Magnite, 10));
        ActivateMoveToPlayer(GameObject.FindGameObjectsWithTag("Fuel"));
        ActivateMoveToPlayer(GameObject.FindGameObjectsWithTag("Coin"));
    }

    public void ActivateBomb()
    {
        if (IsCanUseBonus(Type.Bomb) == false) return;

        MinusBonus(Type.Bomb);
        StartCoroutine(Reload(Type.Bomb, 10));
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Finish");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.TryGetComponent(out DestroyableObject destroyableObject)) destroyableObject.Destroy();
            else Destroy(enemy);
        }
    }

    private void ActivateMoveToPlayer(GameObject[] gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.TryGetComponent(out MoveObject moveObject))
            {
                moveObject.EnableMovingToPlayer(playerCollectSystem.transform);
            }
        }
    }
    private IEnumerator Reload(Type type, float duration)
    {
        Button neededButton = null;

        foreach (Bonus bonus in bonuses)
        {
            if (bonus.Type != type) continue;

            neededButton = bonus.Button;
            break;
        }

        neededButton.interactable = false;
        yield return new WaitForSeconds(duration);
        neededButton.interactable = true;
    }

    [System.Serializable]
    public class Bonus
    {
        public Type Type;
        [HideInInspector] public int Count;
        public TMP_Text CountText;
        public Button Button;
    }
    public enum Type
    {
        Magnite,
        Bomb,
        CoinMultiplier
    }
}