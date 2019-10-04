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
    Slider slider;
    Toggle toggle;
    private void Start()
    {
        audioSouce = GameObject.FindWithTag(musicType.ToString()).GetComponentsInChildren<AudioSource>();
        slider = GetComponentInChildren<Slider>();
        toggle = GetComponentInChildren<Toggle>();
    }
    private void Update()
    {
        foreach(AudioSource a in audioSouce)
        {
            a.volume = slider.value;
            a.mute = !toggle.isOn;
        }
        
    }
}
