using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationSwitch : MonoBehaviour
{
    [SerializeField] private Sprite onSprite, offSprite;
    [SerializeField] private Image image;
    [SerializeField] private Toggle toggle;
    bool isOn;
    private void Start()
    {
        isOn = PlayerPrefs.GetString("Vibration", "On") == "On";
        if (!isOn)
        {
            if (image != null) image.sprite = offSprite;
            if (toggle != null) toggle.SetIsOnWithoutNotify(false);
        }
    }

    public void Switch()
    {
        isOn = !isOn;

        if (isOn)
        {
            if (image != null) image.sprite = onSprite;
            PlayerPrefs.SetString("Vibration", "On");
        }
        else
        {
            if (image != null) image.sprite = offSprite;
            PlayerPrefs.SetString("Vibration", "Off");
        }
    }
}
