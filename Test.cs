using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody2D r;
    // Update is called once per frame
    private void Start()
    {
        r = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.M))
        {
            transform.position = Vector3.zero;

        }
        r.MovePosition(transform.position + Vector3.right * 0.2f);
        
    }
}
