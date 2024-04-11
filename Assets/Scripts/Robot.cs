using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    private float shotReload = 4;

    private void Update()
    {
        shotReload -= Time.deltaTime;

        if (shotReload < 0)
        {
            shotReload = 5;
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
    }

    public void MoveDown()
    {
        transform.DOMoveY(-4, 0.25f).OnComplete(() =>
        {
            if (TryGetComponent(out DestroyableObject destroyableObject))
            {
                Instantiate(destroyableObject.Explosive, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            });
        enabled = false;
    }
    private void OnDestroy()
    {
        AddKillStats();
    }
    private void AddKillStats()
    {
        int kills = PlayerPrefs.GetInt("Kills", 0);
        kills++;
        PlayerPrefs.SetInt("Kills", kills);
    }
}