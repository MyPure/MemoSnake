using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool first = false;
    // Start is called before the first frame update
    void Start()
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
