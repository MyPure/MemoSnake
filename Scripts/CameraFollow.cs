using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;//要跟随的物体
    //public Vector2 preset;//初始偏移量
    public GameObject borden;

    public GameManager gameManager;
    void Start()
    {
        //preset = transform.position - target.transform.position;
        gameManager = GameManager.gameManager;
    }
    void Update()
    {
        Vector2 d = target.transform.position;

        if (borden)
        {
            if (target.transform.position.x <= borden.transform.position.x) d.x = borden.transform.position.x;
            else if (target.transform.position.x >= -borden.transform.position.x) d.x = -borden.transform.position.x;
        }

        if(gameManager.currentMode == 3)
        {
            transform.position = new Vector3(d.x, 0, -10);
        }
        else
        {
            transform.position = new Vector3(d.x, d.y, -10);
        }
    }
}