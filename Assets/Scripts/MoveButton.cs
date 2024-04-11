using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private int speed = 2;
    [SerializeField] private Slider fuelSlider;
    [SerializeField] private Image fuelSliderIconImage;
    [SerializeField] private Sprite warningIconSprite, defaultIconSprite;
    private float fuel;
    private bool isUpMoving = false;
    private Vector2 moveDirection = Vector2.down;
    private const float moveFrame = 2.6f;

    private void Start()
    {
        fuelSlider.maxValue = PlayerPrefs.GetInt("Fuel", 50);
        speed = PlayerPrefs.GetInt("Speed", 3);
        fuelSlider.value = fuelSlider.maxValue;
        fuel = fuelSlider.maxValue;
        playerRigidbody.position = new Vector2(playerRigidbody.position.x, moveFrame);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isUpMoving = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isUpMoving = false;
    }
    private void Update()
    {
        if (isUpMoving)
        {
            fuel -= Time.deltaTime;
            fuelSlider.value = fuel;

            if (fuel < fuelSlider.maxValue / 3 && fuelSliderIconImage.sprite != warningIconSprite)
            {
                fuelSliderIconImage.sprite = warningIconSprite;
            }
        }
    }
    public void AddFuel(float count)
    {
        if (fuel + count > fuelSlider.maxValue) count = fuelSlider.maxValue - fuel;

        fuel += count;
        fuelSlider.DOValue(fuel, 0.1f);
        if (fuel > fuelSlider.maxValue / 3 && fuelSliderIconImage.sprite != defaultIconSprite)
        {
            fuelSliderIconImage.sprite = defaultIconSprite;
        }
    }
    private void FixedUpdate()
    {
        if (playerRigidbody == null) return;

        if (isUpMoving)
        {
            if (fuel > 0 && playerRigidbody.position.y < moveFrame)
            {
                moveDirection = Vector2.up;
            }
            else moveDirection = Vector2.zero;
        }
        else
        {
            /*if (playerRigidbody.position.y > -moveFrame)
            {
                moveDirection = Vector2.down;
            }
            else moveDirection = Vector2.zero;*/

            moveDirection = Vector2.down;
        }


        playerRigidbody.MovePosition(playerRigidbody.position + speed * Time.fixedDeltaTime * moveDirection);
    }
}