using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public Snake snake;
    void Update()
    {
        if(snake.transform.position.x - transform.position.x >= 15)
        {
            transform.Translate(new Vector3(10, 0, 0));
        }
    }
}
