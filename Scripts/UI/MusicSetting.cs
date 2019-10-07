using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSetting : MonoBehaviour
{
    public enum MusicType {
        SoundMusic,
        SoundEffect
    }
    public MusicType musicType;
    public AudioSource[] audioSouce;
    public GameManager gameManager;
    Slider slider;
    Toggle toggle;
    private void Start()
    {
        audioSouce = GameObject.FindWithTag(musicType.ToString()).GetComponentsInChildren<AudioSource>();
        gameManager = GameManager.gameManager;
        slider = GetComponentInChildren<Slider>();
        toggle = GetComponentInChildren<Toggle>();

        slider.value = gameManager.soundVolume[musicType];
        toggle.isOn = !gameManager.soundMute[musicType];
    }
    public void SetVolume()
    {
        gameManager.soundVolume[musicType] = slider.value;
        foreach (AudioSource a in audioSouce)
        {
            a.volume = gameManager.soundVolume[musicType];
        }
    }
    public void SetToggle()
    {
        gameManager.soundMute[musicType] = !toggle.isOn;
        foreach (AudioSource a in audioSouce)
        {
            a.mute = gameManager.soundMute[musicType];
        }
    }
}
