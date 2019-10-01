using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SnakeHead")
        {
            collision.gameObject.GetComponent<Body>().snake.SpeedUp(5);
            Destroy(gameObject);
        }
    }
}
