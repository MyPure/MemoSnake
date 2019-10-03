using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : Prop
{
    public AudioClip audioClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SnakeHead")
        {
            collision.gameObject.GetComponent<Body>().snake.Eat(PropType.Sheild);
            PlaySound(audioClip);
            Destroy(gameObject);
        }
    }
}
