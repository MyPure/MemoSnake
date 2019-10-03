using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public AudioSource audioSource;
    private void Start()
    {
        audioSource = GameObject.Find("PropSound").GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
