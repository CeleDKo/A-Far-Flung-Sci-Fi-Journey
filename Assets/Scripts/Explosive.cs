using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private bool withDestroy;
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.2f).SetUpdate(true).SetEase(Ease.OutBack).OnComplete(() =>
        {
            transform.DOScale(Vector3.zero, 0.2f).SetUpdate(true).OnComplete(() =>
            {
                if (withDestroy) Destroy(gameObject);
                else gameObject.SetActive(false);
            });
        });
    }
}