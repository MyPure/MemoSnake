using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SnakeHead")
        {
            collision.gameObject.GetComponent<Body>().snake.Eat(FoodType.Sheild);
            Destroy(gameObject);
        }
    }
}
