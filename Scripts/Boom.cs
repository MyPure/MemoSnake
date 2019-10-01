using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SnakeHead")
        {
            Body b = collision.gameObject.GetComponent<Body>();
            b.snake.Decrease(b.snake.length / 2);
            Destroy(gameObject);
        }
    }
}
