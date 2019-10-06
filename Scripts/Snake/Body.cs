using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public Body next;//往尾方向
    public Body previous;//往头方向
    public Transform pos;//模拟骨骼
    public Snake snake;//控制身体的snake实例
    public Rigidbody2D rigidbody2D;
    private void Awake()
    {
        pos = transform.GetChild(0).gameObject.transform;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
