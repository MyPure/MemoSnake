using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PropType
{
    Food,
    Mushroom,
    Boom,
    Energy,
    PoisonousGrass,
    Sheild,
    Mode3Food,
    Square
}
public class Prop : MonoBehaviour
{
    public AudioSource audioSource;
    public PropType propType;
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
