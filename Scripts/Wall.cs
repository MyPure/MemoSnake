using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SnakeHead")
        {
            collision.GetComponent<Body>().snake.Die();
        }
    }
}
