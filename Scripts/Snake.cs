using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public static float minDistance = 0.4f;
    public static float speed = 5;
    public int length = 0;
    public GameObject BodyPrefab;
    public GameObject HeadPrefab;
    Body p1, p2, head, tail;

    void Start()
    {
        p1 = p2 = head = Instantiate(HeadPrefab, transform.position, Quaternion.identity).GetComponent<Body>();
        length++;
        head.snake = this;

        for (int i = 0; i < 2; i++)
        {
            p1 = Instantiate(BodyPrefab, transform.position - Vector3.up * minDistance * (i + 1), Quaternion.identity).GetComponent<Body>();
            p1.snake = this;
            p1.previous = p2;
            p2.next = p1;
            p2 = p1;
            length++;
        }

        tail = p1;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        //头的旋转
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        float angle;
        if ((head.transform.position - mousePosition).magnitude > minDistance * 4)
        {
            angle = Vector2.Angle(Vector2.up, mousePosition - head.transform.position);
            if (mousePosition.x > head.transform.position.x)
            {
                angle *= -1;
            }
            head.transform.eulerAngles = new Vector3(0, 0, angle);
        }
        //身体的旋转
        Body b = head.next;
        while (b != null)
        {
            angle = Vector2.Angle(Vector2.up, b.previous.pos.position - b.transform.position);
            if (b.previous.transform.position.x > b.transform.position.x)
            {
                angle *= -1;
            }
            b.transform.eulerAngles = new Vector3(0, 0, angle);
            b = b.next;
        }

        //整体的移动
        b = head;
        while (b != null)
        {
            if (b == head)
            {
                head.transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
            else
            {
                if ((b.transform.position - b.previous.pos.position).magnitude > minDistance)
                {
                    Vector3 d = Vector3.MoveTowards(b.transform.position, b.previous.pos.position, speed * Time.deltaTime);
                    b.transform.position = d;
                }
                Debug.DrawLine(b.transform.position, b.previous.pos.position, Color.red);

            }
            b = b.next;
        }
    }

    /// <summary>
    /// 伸长n节
    /// </summary>
    /// <param name="n">减少的节数</param>
    public void Extend(int n)
    {
        p2 = tail;

        for(int i = 0; i < n; i++)
        {
            p1 = Instantiate(BodyPrefab, p2.transform.position + (p2.pos.position - p2.transform.position).normalized * minDistance * (i + 1), Quaternion.identity).GetComponent<Body>();
            p1.snake = this;
            p1.previous = p2;
            p2.next = p1;
            p2 = p1;
        }
        tail = p1;
        p1 = p2 = null;
        length += n;
    }

    /// <summary>
    /// 减少n节
    /// </summary>
    /// <param name="n">减少的节数</param>
    public void Decrease(int n)
    {
        if(length - n <= 0)
        {
            return;
        }
        for(int i = 0; i < n; i++)
        {
            p2 = tail;
            p1 = p2.previous;
            tail = p1;
            Destroy(p2.gameObject);
            p2 = p1;
        }
        p1 = p2 = null;
        length -= n;
    }
}

