using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private int healths = 1;
    [SerializeField] private GameObject explosive;
    [SerializeField] private Image healthFill;
    public GameObject Explosive => explosive;
    public UnityEvent OnDestroying;
    private bool isVisible = false;
    private int maxHealths;
    private void Start()
    {
        maxHealths = healths;
    }

    public void SetHealthBar(Image healthFill)
    {
        this.healthFill = healthFill;
        this.healthFill.fillAmount = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerProjectile") && isVisible)
        {
            Instantiate(explosive, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            int damage = PlayerPrefs.GetInt("Damage", 1);

            healths -= damage;
            if (healthFill != null) healthFill.DOFillAmount((float)healths / maxHealths, 0.1f);
            if (healths <= 0) Destroy();
        }
    }
    public void Destroy()
    {
        OnDestroying.Invoke();
        Instantiate(explosive, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
    }
    private void OnBecameVisible()
    {
        isVisible = true;
    }
}