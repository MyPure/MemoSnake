using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SnakeHead")
        {
            collision.gameObject.GetComponent<Body>().snake.Extend(1);
            Destroy(gameObject);
        }
    }
}
