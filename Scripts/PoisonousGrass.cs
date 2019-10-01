using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousGrass : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SnakeHead")
        {
            collision.gameObject.GetComponent<Body>().snake.Eat(FoodType.PoisonousGrass);
            Destroy(gameObject);
        }
    }
}
