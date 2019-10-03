using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PropType
{
    Food,
    Mushroom,
    Boom,
    Energy,
    PoisonousGrass,
    Sheild
}

public class Snake : MonoBehaviour
{
    public static float minDistance = 0.3f;
    public static float speed = 5;
    public int length = 0;
    public GameObject BodyPrefab;
    public GameObject HeadPrefab;
    bool isSpeedUp = false;//是否在加速
    float speedUpBaseTime;//加速开始时间
    bool isSheild;//是否处于护盾
    public GameObject sheildCirclePrefab;//护盾光环
    public float sheildBaseTime;//护盾开始时间
    Body p1, p2, head, tail;
    public GameObject mode1DeathUI;
    public bool death = false;

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
        if (!death)
        {
            Move();
        }
        transform.position = head.transform.position;
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
    /// 吃不同道具的反应
    /// </summary>
    /// <param name="PropType">吃的道具的类型</param>
    public void Eat(PropType PropType)
    {
        switch (PropType)
        {
            case PropType.Food:Increase(1);break;
            case PropType.Mushroom:Increase(length);break;
            case PropType.PoisonousGrass:
                {
                    if (isSheild)
                    {
                        isSheild = false;
                        break;
                    }
                    Decrease(2);
                    break;
                }
            case PropType.Boom:
                {
                    if (isSheild)
                    {
                        isSheild = false;
                        break;
                    }
                    Decrease(length / 2);
                    break;
                }
            case PropType.Energy:SpeedUp(5);break;
            case PropType.Sheild:Sheild(5);break;
        }
    }

    /// <summary>
    /// 伸长n节
    /// </summary>
    /// <param name="n">减少的节数</param>
    void Increase(int n)
    {
        p2 = tail;

        for(int i = 0; i < n; i++)
        {
            p1 = Instantiate(BodyPrefab, p2.transform.position + (p2.pos.position - p2.transform.position).normalized * minDistance, Quaternion.identity).GetComponent<Body>();

            float angle = Vector2.Angle(Vector2.up, p2.pos.position - p1.transform.position);
            if (p2.pos.position.x > p1.transform.position.x)
            {
                angle *= -1;
            }
            p1.transform.eulerAngles = new Vector3(0, 0, angle);

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
    void Decrease(int n)
    {
        if(length - n <= 0)
        {
            Die();
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

    /// <summary>
    /// 加速
    /// </summary>
    /// <param name="time">持续时间</param>
    void SpeedUp(float time)
    {
        speedUpBaseTime = Time.time;
        if (!isSpeedUp)
        {
            StartCoroutine(StartSpeedUp(time));
        }
    }

    IEnumerator StartSpeedUp(float lastTime)
    {
        isSpeedUp = true;
        float basespeed = speed;
        speed *= 1.5f;
        while(Time.time - speedUpBaseTime < lastTime)
        {
            yield return null;
        }
        speed = basespeed;
        isSpeedUp = false;
    }

    /// <summary>
    /// 护盾
    /// </summary>
    /// <param name="time">持续时间</param>
    void Sheild(float time)
    {
        sheildBaseTime = Time.time;
        if (!isSheild)
        {
            StartCoroutine(StartSheild(time));
        }
    }

    IEnumerator StartSheild(float lastTime)
    {
        isSheild = true;
        GameObject s = Instantiate(sheildCirclePrefab, head.gameObject.transform);
        while(Time.time - sheildBaseTime < lastTime && isSheild)
        {
            yield return null;
        }
        isSheild = false;
        Destroy(s);
    }

    public void Die()
    {
        Instantiate(mode1DeathUI);
        death = true;
    }
}

