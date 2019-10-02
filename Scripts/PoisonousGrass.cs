using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousGrass : Prop
{
    public AudioClip audioClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SnakeHead")
        {
            collision.gameObject.GetComponent<Body>().snake.Eat(PropType.PoisonousGrass);
            PlaySound(audioClip);
            Destroy(gameObject);
        }
    }
}
