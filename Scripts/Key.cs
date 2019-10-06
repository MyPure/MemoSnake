using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Prop
{
    public GameObject door;
    public AudioClip audioClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SnakeHead")
        {
            Destroy(door);
            PlaySound(audioClip);
            Destroy(gameObject);
        }
    }
}
