using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSwitch : MonoBehaviour
{
    [SerializeField] private Sprite soundOnSprite, soundOffSprite;
    [SerializeField] private Image soundImage;
    [SerializeField] private Toggle toggle;
    bool soundIsOn;
    private void Start()
    {
        soundIsOn = PlayerPrefs.GetString("Sound", "On") == "On";
        if (!soundIsOn)
        {
            if (soundImage != null) soundImage.sprite = soundOffSprite;
            if (toggle != null) toggle.SetIsOnWithoutNotify(false);
        }
    }

    public void Switch()
    {
        soundIsOn = !soundIsOn;

        if (soundIsOn)
        {
            if (soundImage != null) soundImage.sprite = soundOnSprite;
            PlayerPrefs.SetString("Sound", "On");
            MusicManager.Unmute();
        }
        else
        {
            if (soundImage != null) soundImage.sprite = soundOffSprite;
            PlayerPrefs.SetString("Sound", "Off");
            MusicManager.Mute();
        }
    }
}
