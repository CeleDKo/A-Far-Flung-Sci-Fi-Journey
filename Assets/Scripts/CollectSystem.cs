using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CollectSystem : MonoBehaviour
{
    [SerializeField] private Explosive explosive;
    [SerializeField] private GameObject x2;
    [SerializeField] private Balance balance;
    [SerializeField] private GameGeneration gameGeneration;
    public UnityEvent<int> OnCoinCollect;
    public UnityEvent OnFuelCollect, OnDamage;
    private bool isMultiplier;
    public bool IsMultiplier => isMultiplier;
    private void Start()
    {
        OnCoinCollect.AddListener(balance.Add);
        OnCoinCollect.AddListener(gameGeneration.CoinCollected);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerProjectile")) return;

        collision.collider.enabled = false;

        if (collision.collider.CompareTag("Coin"))
        {
            OnCoinCollect.Invoke(isMultiplier ? 2 : 1);
            DestroyObject(collision.transform);
        }
        else if (collision.collider.CompareTag("Fuel"))
        {
            OnFuelCollect.Invoke();
            DestroyObject(collision.transform);
        }
        else if (collision.collider.CompareTag("Finish"))
        {
            explosive.transform.position = collision.GetContact(0).point;
            explosive.gameObject.SetActive(true);

            OnDamage.Invoke();
            DestroyObject(transform, 0.2f);
        }
    }
    private void OnBecameInvisible()
    {
        OnDamage.Invoke();
    }
    public void ActivateMultiplierCoin()
    {
        if (isMultiplier) return;

        StartCoroutine(MultiplierCoin());
    }
    private IEnumerator MultiplierCoin()
    {
        isMultiplier = true;
        x2.SetActive(true);
        yield return new WaitForSeconds(10);
        isMultiplier = false;
        x2.SetActive(false);
        yield break;
    }
    private void DestroyObject(Transform transform, float delay = 0)
    {
        transform.DOScale(Vector2.zero, 0.1f).SetUpdate(true).SetDelay(delay).OnComplete(() =>
        {
            Destroy(transform.gameObject);
        });
    }
}