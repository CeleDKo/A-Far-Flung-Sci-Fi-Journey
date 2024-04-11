using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    [SerializeField] private Transform arrow, rotate;
    [SerializeField] private GameObject winningWindow;
    [SerializeField] private Image winningWindowIconImage;
    [SerializeField] private Sprite bombSprite, coinSprite, magniteSprite;
    [SerializeField] private TMP_Text spinButtonText, winningWindowText;
    [SerializeField] private BonusesManager bonusesManager;
    private bool isSpinning;

    private void OnEnable()
    {
        rotate.eulerAngles = Vector3.zero;
    }

    public void StartSpin()
    {
        if (isSpinning) return;
    
        Vector3 neededRotation = new Vector3(0,0, rotate.eulerAngles.z - Random.Range(180f, 360 * 1.5f));
        float duration = 3f;

        isSpinning = true;
        spinButtonText.text = "Spinning";
        rotate.DORotate(neededRotation, duration, RotateMode.FastBeyond360).SetEase(Ease.OutBack).OnComplete(() =>
        {
            RaycastHit2D hit2D = Physics2D.Raycast(arrow.position, Vector2.up, 1);
            if (hit2D)
            {
                string hitName = hit2D.collider.gameObject.name;
                Debug.Log(hitName + "\nNeeded Z Rotation - " + neededRotation);

                switch (hitName)
                {
                    case "Bomb":
                        winningWindowIconImage.sprite = bombSprite;
                        winningWindowText.text = "+1 bomb bonus";
                        bonusesManager.AddBonus(BonusesManager.Type.Bomb);
                        break;
                    case "Coin":
                        winningWindowIconImage.sprite = coinSprite;
                        winningWindowText.text = "+1 coin bonus";
                        bonusesManager.AddBonus(BonusesManager.Type.CoinMultiplier);
                        break;
                    case "Magnite":
                        winningWindowIconImage.sprite = magniteSprite;
                        winningWindowText.text = "+1 magnate bonus";
                        bonusesManager.AddBonus(BonusesManager.Type.Magnite);
                        break;
                }
                winningWindow.SetActive(true);
            }
            spinButtonText.text = "Spin";
            isSpinning = false;
        });
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(arrow.position, Vector2.up * 1);
    }
    private void OnDisable()
    {
        rotate.DOKill();
    }
}
