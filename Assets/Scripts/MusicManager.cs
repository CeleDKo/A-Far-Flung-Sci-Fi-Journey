using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private string musicTag = "Music";
    private AudioSource audioSource;
    private static MusicManager instance;
    private const string key = "Sound";
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(musicTag);

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (instance == null) instance = this;
    }
    private void Start()
    {
        audioSource.mute = PlayerPrefs.GetString(key, "On") == "Off";
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", audioSource.volume);
    }
    public static void SetVolume(float neededVolume)
    {
        instance.audioSource.volume = neededVolume;
        PlayerPrefs.SetFloat("MusicVolume", instance.audioSource.volume);
    }
    public static void Mute()
    {
        instance.audioSource.mute = true;
        PlayerPrefs.SetString(key, "Off");
    }
    public static void Unmute()
    {
        instance.audioSource.mute = false;
        PlayerPrefs.SetString(key, "On");
    }
}