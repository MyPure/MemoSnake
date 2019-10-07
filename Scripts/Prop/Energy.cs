using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : Prop
{
    public AudioClip audioClip;
    void Start()
    {
        propType = PropType.Energy;
        audioSource = GameObject.Find("PropSound").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SnakeHead")
        {
            collision.gameObject.GetComponent<Body>().snake.Eat(this);
            PlaySound(audioClip);
            Destroy(gameObject);
        }
    }
}
