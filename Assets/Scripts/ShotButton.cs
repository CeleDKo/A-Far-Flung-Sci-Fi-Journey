using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShotButton : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private Transform player;
    private float reload;
    private const float reloadDuration = 0.25f;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Shot);
        reload = reloadDuration;
    }
    private void Shot()
    {
        if (reload > 0) return;

        Instantiate(spawnObject, player.position, Quaternion.identity);
        reload = reloadDuration;
    }
    private void Update()
    {
        if (reload > 0)
        {
            reload -= Time.deltaTime;
        }
    }
}