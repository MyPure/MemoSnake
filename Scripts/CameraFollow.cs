using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;//要跟随的物体
    public Vector2 preset;//初始偏移量
    public GameObject borden;
    void Start()
    {
        preset = transform.position - target.transform.position;

    }
    void Update()
    {
        Vector2 d = (Vector2)target.transform.position + preset;

        //x方向上的跟随
        if (target.transform.position.x <= borden.transform.position.x) d.x = borden.transform.position.x;
        else if (target.transform.position.x >= -borden.transform.position.x) d.x = -borden.transform.position.x;

        float xMove = Mathf.Min(1, Mathf.Abs(d.x - transform.position.x) / 2f) * 6 * Time.deltaTime;
        if (transform.position.x <= d.x)
        {
            transform.position += new Vector3(xMove, 0, 0);
        }
        else
        {
            transform.position -= new Vector3(xMove, 0, 0);
        }

        //y方向上的跟随
        if (target.transform.position.y >= borden.transform.position.y) d.y = borden.transform.position.y;
        else if (target.transform.position.y <= -borden.transform.position.y) d.y = -borden.transform.position.y;

        float yMove = Mathf.Min(1, Mathf.Abs(d.y - transform.position.y) / 2f) * 6 * Time.deltaTime;
        if (transform.position.y <= d.y)
        {
            transform.position += new Vector3(0, yMove, 0);
        }
        else
        {
            transform.position -= new Vector3(0, yMove, 0);
        }
    }
}