using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static bool first = false;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (!first)
        {
            first = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
