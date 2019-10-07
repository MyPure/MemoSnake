using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Prop
{
    public AudioClip audioClip;
    void start()
    {
        propType = PropType.Mushroom;
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
