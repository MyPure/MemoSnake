using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SnakeHead")
        {
            Body b = collision.gameObject.GetComponent<Body>();
            b.snake.Increase(b.snake.length);
            Destroy(gameObject);
        }
    }
}
